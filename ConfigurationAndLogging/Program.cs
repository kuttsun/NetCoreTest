using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

using NLog.Extensions.Logging;


namespace ConfigurationAndLogging
{
    public class Program
    {
        // https://stackoverflow.com/questions/38706959/net-core-console-applicatin-configuration-xml
        static void Main(string[] args)
        {
            // DI を使った Application クラスを例として挙げる

            // IServiceCollection に対してフレームワークが提供する拡張メソッド を使いながら依存性を定義してゆき、
            // ActivatorUtilities などを使って依存関係が解決されたインスタンスを取得するのが大まかな流れ
            // add メソッドでサービスを追加すると、DI として構成される
            IServiceCollection serviceCollection = new ServiceCollection();

            // ConfigureServices で DI の準備を行う
            ConfigureServices(serviceCollection);

            // var app = new Application(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // DI サービスコンテナから指定した型のサービスを取得する
            var app = serviceProvider.GetService<Application>();

            // 実行
            app.Run();
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ロギングの設定
            ILoggerFactory loggerFactory = new LoggerFactory()
                // コンソールに出力する
                .AddConsole()
                // Visual Studio のデバッグウィンドウに出力する
                .AddDebug();
            // NLogプロバイダーを追加することで、NLogの出力も行う
            // ただし、プロジェクトに「NLog.config」を追加しておくこと（プロパティで「出力ディレクトリにコピー」を有効にする必要あり）
            loggerFactory.AddProvider(new NLogLoggerProvider());

            // DI サービスコンテナに Singleton ライフサイクルにてオブジェクトを登録する
            // Singleton ライフサイクルでは Dependency インスタンスを一つ生成し、そのインスタンスをアプリケーションで共有する
            services.AddSingleton(loggerFactory);
            // AddLogging メソッドを呼び出すことで ILoggerFactory と ILogger<T> が DI 経由で扱えるようになる
            services.AddLogging();

            // IConfigurationBuilder で設定を選択
            // IConfigurationBuilder.Build() で設定情報を確定し、IConfigurationRoot を生成する
            IConfigurationRoot configuration = new ConfigurationBuilder()
                // 基準となるパスを設定
                .SetBasePath(Directory.GetCurrentDirectory())
                // ここでどの設定元を使うか指定
                // 同じキーが設定されている場合、後にAddしたものが優先される
                .AddJsonFile($"appsettings.json", optional: true)
                // ここでは JSON より環境変数を優先している
                .AddEnvironmentVariables()
                // 上記の設定を実際に適用して構成読み込み用のオブジェクトを得る
                .Build();

            // Logger と同じく DI サービスコンテナに Singleton ライフサイクルにてオブジェクトを登録する
            services.AddSingleton(configuration);

            // オプションパターンを有効にすることで、構成ファイルに記述した階層構造データを POCO オブジェクトに読み込めるようにする
            services.AddOptions();

            // Configure<T> を使ってオプションを初期化する
            // IConfigurationRoot から GetSection 及び GetChildren で個々の設定の取り出しができる
            // ここでは "MyOptions" セクションの内容を MyOptions として登録
            services.Configure<MyOptions>(configuration.GetSection(nameof(MyOptions)));
            services.Configure<Account>(configuration.GetSection($"{nameof(MyOptions)}:{nameof(Account)}"));
            services.Configure<MyOptions2>(configuration.GetSection(nameof(MyOptions2)));

            // Application を DI サービスコンテナに登録する
            // AddTransient はインジェクション毎にインスタンスが生成される
            services.AddTransient<Application>();
        }
    }

    // オプションを保持するためのクラス
    public class MyOptions
    {
        public string Str { get; set; }
        public Account Account { get; set; }
    }

    public class Account
    {
        public List<User> Users { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class MyOptions2
    {
        public string Str { get; set; }
    }

    public class Application
    {
        ILogger logger;
        readonly MyOptions settings;
        readonly MyOptions2 settings2;
        readonly Account account;

        // コンストラクタの引数として ILogger や IOptions 型の引数を定義すると、.NET Core の DI 機能によりオブジェクトが注入される
        // (コンストラクタインジェクション)
        // 設定を取得する際には IOptions<T> を経由して DI から値を受け取る
        public Application(ILogger<Application> logger, IOptions<MyOptions> settings, IOptions<Account> account, IOptions<MyOptions2> settings2)
        {
            this.logger = logger;
            // ここで受け取れるオブジェクトは、オブジェクト自体ではなくアクセサオブジェクトであるため、Value プロパティを参照している
            this.settings = settings.Value;
            //this.account = settings.Value.Account;
            this.account = account.Value;
            this.settings2 = settings2.Value;
        }

        public void Run()
        {
            logger.LogCritical("Log Critical");
            logger.LogError("Log Error");
            logger.LogWarning("Log Warning");
            logger.LogInformation("Log Information");
            // 以下の２つはデフォルトでは出力されない
            logger.LogDebug("Log Debug");
            logger.LogTrace("Log Trace");

            try
            {
                logger.LogInformation(settings.Str);
                foreach (var account in settings.Account.Users)
                {
                    logger.LogInformation($"Name:{account.Name}, Password:{account.Password}");
                }
                foreach (var account in account.Users)
                {
                    logger.LogInformation($"Name:{account.Name}, Password:{account.Password}");
                }
                logger.LogInformation(settings2.Str);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }

            // 他クラスにILoggerを渡してログ出力を試す
            (new Beta(logger)).Execute();
        }
    }

    public class Beta
    {
        ILogger logger;

        public Beta(ILogger logger)
        {
            this.logger = logger;
        }

        public void Execute()
        {
            logger.LogCritical("test");
        }
    }
}
