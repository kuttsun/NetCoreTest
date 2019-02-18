using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    enum Hoge
    {
        Foo,
        Bar
    };

    class Program
    {
        static void Main(string[] args)
        {
            // header
            Console.Write($"lot |");
            for (int benefit = 1; benefit <= 10; benefit++)
            {
                Console.Write($"{benefit,4}|");
                //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            }
            Console.WriteLine();
            Console.WriteLine($"----|----|----|----|----|----|----|----|----|----|----|");
            // body
            for (double lot = 0.01; lot <= 0.10; lot += 0.01)
            {
                GetMinSpread(lot);
            }
            GetMinSpread(1);

            Console.ReadKey();
        }

        static void GetMinSpread(double lot)
        {
            Console.Write($"{lot:N2}|");
            for (int benefit = 1; benefit <= 10; benefit++)
            {
                var minSpread = Math.Ceiling((0.5 + benefit - 1) / lot);
                Console.Write($"{minSpread,4}|");
                //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            }
            Console.WriteLine();
        }

        static int GetMinSpread(double lot, int benefit)
        {
            // bitflyer では端数は四捨五入されるようなので、
            // 1 円の利益を得るためには、差額×ロットが 0.5 円以上でなければならない
            // 尚、+1は確実に切り上げされるための補正、+2は指値価格の補正分
            var minSpread = (int)((0.5 + benefit - 1) / lot) + 1;
            return minSpread;
        }

        /// <summary>
        /// BitFlyer の板情報のテスト
        /// </summary>
        static void GetBitFlyerBoard()
        {
            // スナップショット
            var snapshot = new List<PriceAndSize>()
            {
                new PriceAndSize(){ Price=1000, Size = 1 },
                new PriceAndSize(){ Price=1001, Size = 2 },
                new PriceAndSize(){ Price=1002, Size = 3 },
                new PriceAndSize(){ Price=1003, Size = 4 },
                new PriceAndSize(){ Price=1004, Size = 5 },
                new PriceAndSize(){ Price=1005, Size = 6 },
                new PriceAndSize(){ Price=1006, Size = 7 },
                new PriceAndSize(){ Price=1007, Size = 8 },
                new PriceAndSize(){ Price=1008, Size = 9 },
                new PriceAndSize(){ Price=1009, Size = 10 },
            };

            // 差分情報
            var board = new List<PriceAndSize>()
            {
                new PriceAndSize(){ Price=1000, Size = 0 },
                new PriceAndSize(){ Price=1001, Size = 1 },
                new PriceAndSize(){ Price=1003, Size = 1 },
                new PriceAndSize(){ Price=1005, Size = 1 },
                new PriceAndSize(){ Price=1006, Size = 0 },
                new PriceAndSize(){ Price=1007, Size = 1 },
                new PriceAndSize(){ Price=1008, Size = 0 },
                new PriceAndSize(){ Price=1009, Size = 1 },
                new PriceAndSize(){ Price=1010, Size = 2 },
            };

            // サイズが0のものは削除し、新たに増えた情報を追加
            // ・現在保持している板情報のうち、差分情報と被るものは除外する（差分情報側を優先するため）
            var asks = snapshot.Where(x => !board.Any(y => y.Price == x.Price)).ToList();
            foreach (var ask in asks)
            {
                Console.WriteLine($"Price:{ask.Price}, Size:{ask.Size}");
            }
            Console.WriteLine($"-----");
            // ・差分情報はサイズが0以外のものは全て採用する
            asks.AddRange(board.Where(x => x.Size != 0));
            foreach (var ask in asks)
            {
                Console.WriteLine($"Price:{ask.Price}, Size:{ask.Size}");
            }
            Console.WriteLine($"-----");
            // ソート
            asks = asks.OrderBy(x => x.Price).Distinct().ToList();
            foreach (var ask in asks)
            {
                Console.WriteLine($"Price:{ask.Price}, Size:{ask.Size}");
            }
            Console.ReadKey();
        }

        struct PriceAndSize
        {
            public int Price { get; set; }
            public double Size { get; set; }
        }
    }
}
