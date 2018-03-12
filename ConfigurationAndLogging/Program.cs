using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
            var app = serviceProvider.GetService<Sample>();

            // 実行
            app.Start();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            // 新しい ILogger インスタンスを作成（カテゴリ名 default）
            var logger = loggerFactory.CreateLogger("Default");
            logger.LogInformation("終了");
            Console.ReadKey();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // ロギングの設定
            ILoggerFactory loggerFactory = new LoggerFactory()
                // コンソールに出力する
                //.AddConsole()
                // Visual Studio のデバッグウィンドウに出力する
                .AddDebug()
                // NLog を使用する
                .AddNLog();
            //loggerFactory.ConfigureNLog("NLog.Config");

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
            services.AddTransient<Sample>();
        }
    }
}
