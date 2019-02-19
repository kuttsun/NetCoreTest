using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            // DI サービスへの登録
            services.AddSingleton(typeof(IThing<Class1>), typeof(ConcreteClass1));
            services.AddSingleton<IFoo, ConcreteClass1>();
            services.AddSingleton<Class1>();
            services.AddSingleton<Class2>();
            //services.AddSingleton<ConcreteClass1>();
            //services.AddSingleton<ConcreteClass2>();
            services.AddSingleton<DiClass1>();

            // DI サービスのビルド
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //serviceProvider.GetService<ConcreteClass1>();
            //serviceProvider.GetService<ConcreteClass2>();
            serviceProvider.GetService<DiClass1>();
            //serviceProvider.GetService<Class2>();

            Console.ReadKey();
        }
    }
}
