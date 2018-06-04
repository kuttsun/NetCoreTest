using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigurationAndLogging
{
    public class Sample
    {
        ILogger logger;

        readonly MyOptions settings;
        readonly MyOptions2 settings2;
        readonly Account account;

        // コンストラクタの引数として ILogger や IOptions 型の引数を定義すると、.NET Core の DI 機能によりオブジェクトが注入される
        // (コンストラクタインジェクション)
        // 設定を取得する際には IOptions<T> を経由して DI から値を受け取る
        public Sample(ILogger<Sample> logger, ILoggerFactory loggerFactory,IOptions<MyOptions> settings, IOptions<Account> account, IOptions<MyOptions2> settings2)
        {
            //this.logger = loggerFactory.CreateLogger("Custom");
            this.logger = logger;
            // ここで受け取れるオブジェクトは、オブジェクト自体ではなくアクセサオブジェクトであるため、Value プロパティを参照している
            this.settings = settings.Value;
            //this.account = settings.Value.Account;
            this.account = account.Value;
            this.settings2 = settings2.Value;
        }

        public void Start()
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
