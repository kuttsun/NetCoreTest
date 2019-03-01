using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BitFlyer.Spread();

            // 四捨五入のテスト
            //var target = 0.041936184;
            //var hoge = Math.Round(target, 8, MidpointRounding.AwayFromZero);
            //Console.WriteLine($"{target}\n{hoge}");

            Console.ReadKey();
        }
    }
}
