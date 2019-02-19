using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection2
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            // DI サービスへの登録
            services.AddSingleton<ImplClass>();
            services.AddSingleton<IFoo>(x => x.GetService<ImplClass>());
            services.AddSingleton<IBar>(x => x.GetService<ImplClass>());
            //services.AddSingleton<IFoo, ImplClass>();
            //services.AddSingleton<IBar, ImplClass>();
            services.AddSingleton<Class1>();
            services.AddSingleton<Class2>();

            // DI サービスのビルド
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Class1>();
            serviceProvider.GetService<Class2>();

            Console.ReadKey();
        }
    }
}

