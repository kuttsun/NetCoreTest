using System;
using System.IO;

using Microsoft.Extensions.Configuration;
// Microsoft.Extensions.Configuration.Json と Microsoft.Extensions.Configuration.EnvironmentVariables のインストールも必要


namespace NetCoreConsoleApp
{
    class Configuration
    {
        public Configuration()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(@"appSetting.json", true);
            builder.AddEnvironmentVariables();

            var config = builder.Build();
            Console.Write($"Hello, {config["appSettings:frameworkName"]}"); // "Hello, .NET Core"
        }

    }
}
