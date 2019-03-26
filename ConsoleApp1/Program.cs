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

            // expected: None
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 23, 54, 59)));
            // expected: Before
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 23, 55, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 23, 58, 59)));
            // expected: Halfway
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 23, 59, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 26, 0, 0, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 26, 0, 0, 59)));
            // expected: After
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 26, 0, 1, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 26, 0, 4, 59)));
            // expected: None
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 26, 0, 5, 0)));

            Console.WriteLine("-----");

            // expected: None
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 3, 54, 59)));
            // expected: Before
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 3, 55, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 3, 58, 59)));
            // expected: Halfway
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 3, 59, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 4, 0, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 4, 10, 59)));
            // expected: After
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 4, 11, 0)));
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 4, 14, 59)));
            // expected: None
            Console.WriteLine(BitFlyer.IsBreakTime(new DateTime(2019, 3, 25, 4, 15, 0)));
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
