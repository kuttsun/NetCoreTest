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

            //Person person = null;
            ////Console.WriteLine($"Name:{person?.Name}");


            //int age = (int)person?.Age;

            // TimeSpan には上限を超えた値も入力可能（90秒とか）
            var second = 15;// 60 * 60 * 24 * 7;
            var ts = new TimeSpan(0, 0, second);
            Console.WriteLine(ts.TotalSeconds);

            //var begin = new DateTime(2019, 12, 31, 12, 00, 00);
            //var end = begin.AddYears(1);
            //var current = begin;
            //while (current < end)
            //{
            //    var diff = current - DateTime.MinValue;
            //    if (diff.TotalSeconds % second == 0)
            //    {
            //        Console.WriteLine(current);
            //    }
            //    current = current.AddSeconds(1);
            //}

            var begin = new DateTime(2019, 12, 31, 12, 00, 00);
            var end = begin.AddMinutes(1);
            // ターゲットの用意
            var targets = new List<DateTime>();
            var current = begin;
            while (current < end)
            {
                targets.Add(current);
                current = current.AddSeconds(1);
            }
            // 確認
            foreach (var target in targets) Console.WriteLine(target);
            // 5秒ごとにまとめる
            var terms = new List<DateTime>();
            foreach (var target in targets)
            {
                var diff = target - DateTime.MinValue;
                if (diff.TotalSeconds % second == 0)
                {
                    // まとめた結果を表示
                    if (terms.Any())
                    {
                        Console.WriteLine("----------");
                        foreach (var term in terms) Console.WriteLine(term);
                    }
                    // 新たな区間の開始
                    terms.Clear();
                }
                terms.Add(target);
            }
            // 作成途中の区間がある
            if(terms.Any())
            {
                Console.WriteLine("-----current-----");
                foreach (var term in terms) Console.WriteLine(term);
            }


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
