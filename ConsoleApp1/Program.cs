using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //BitFlyer.Spread();

            // 四捨五入のテスト
            //var target = 0.041936184;
            //var hoge = Math.Round(target, 8, MidpointRounding.AwayFromZero);
            //Console.WriteLine($"{target}\n{hoge}");

            // 排他処理のテスト
            //var lockTest = new LockTest();
            //Parallel.For(0, 99, i =>
            //{
            //    lockTest.Hoge(i);
            //});
            //Console.WriteLine("完了");
            //Console.ReadKey();

            //var discord = new Discord("Webhookurl");
            //discord.Send("test");

            Person person = null;
            //Console.WriteLine($"Name:{person?.Name}");


            int age = (int)person?.Age;

            Console.ReadKey();
        }
    }

    class LockTest
    {
        object lockObject = new object();
        int count = 0;

        public void Hoge(int i)
        {
            bool acquiredLock = false;

            try
            {
                Monitor.TryEnter(lockObject, 0, ref acquiredLock);
                if (acquiredLock)
                {
                    // ロック取得に成功したときの処理
                    count++;
                    Console.WriteLine($"i={i}, {count}");
                    Thread.Sleep(100);
                }
                else
                {
                    // Console.WriteLine($"ロック取得失敗");
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(lockObject);
            }
        }
    }
}
