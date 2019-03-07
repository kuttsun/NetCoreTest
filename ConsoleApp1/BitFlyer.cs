using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    class BitFlyer
    {
        /// <summary>
        /// スプレッドの計算
        /// </summary>
        public static void Spread()
        {
            //var lot = 0.03;
            //var minSpread = GetMinSpreadSub(lot, 1);
            //Console.WriteLine($"minSpread={minSpread}, Round={Math.Round((minSpread) * lot)}, Round+1={Math.Round((minSpread + 1) * lot)}");

            //// header
            //Console.Write($"lot |");
            //for (int benefit = 1; benefit <= 10; benefit++)
            //{
            //    Console.Write($"{benefit,4}|");
            //    //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            //}
            //Console.WriteLine();
            //Console.WriteLine($"----|----|----|----|----|----|----|----|----|----|----|");
            //// body
            //for (double lot = 0.01; lot <= 0.10; lot += 0.01)
            //{
            //    GetProfitSpread(lot);
            //}
            //GetProfitSpread(0.5);
            //GetProfitSpread(1);

            //Console.WriteLine();

            //// header
            //Console.Write($"lot |");
            //for (int benefit = 1; benefit <= 10; benefit++)
            //{
            //    Console.Write($"{benefit,4}|");
            //    //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            //}
            //Console.WriteLine();
            //Console.WriteLine($"----|----|----|----|----|----|----|----|----|----|----|");
            //// body
            //for (double lot = 0.01; lot <= 0.10; lot += 0.01)
            //{
            //    GetLossSpread(lot);
            //}
            //GetProfitSpread(0.5);
            //GetLossSpread(1);


            var lot = 0.02;
            var price = 100;
            var profit = GetMinPriceToObtainSameProfit(lot, price);
            Console.WriteLine($"{profit}");
            var loss = GetMaxPriceToObtainSameLoss(lot, price);
            Console.WriteLine($"{loss}");
        }

        static void GetProfitSpread(double lot)
        {
            Console.Write($"{lot:N2}|");
            for (int benefit = 1; benefit <= 10; benefit++)
            {
                var minSpread = GetProfitMinSpread(lot, benefit);
                Console.Write($"{minSpread,4}|");
                //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            }
            Console.WriteLine();
        }

        static void GetLossSpread(double lot)
        {
            Console.Write($"{lot:N2}|");
            for (int loss = 1; loss <= 10; loss++)
            {
                var minSpread = GetLossMaxSpread(lot, loss);
                Console.Write($"{minSpread,4}|");
                //Console.WriteLine($"lot={lot:N2}, benefit={benefit,2}, minSpread={minSpread,4}");
            }
            Console.WriteLine();
        }

        static int GetProfitMinSpread(double lot, int benefit)
        {
            // bitflyer では端数は四捨五入（最近接偶数への丸め）されるようなので、
            // 1 円の利益を得るためには、差額×ロットが 0.51 円以上でなければならない
            // ※0.50円だと「最近接偶数への丸め」により、四捨五入すると0になってしまう
            var minSpread = (int)Math.Ceiling((0.5 + benefit - 1) / lot);
            // ここで「最近接偶数への丸め」を行い、期待値の利益にならないようであれば+1して、「最近接偶数への丸め」でも期待値の利益になるようにする
            if (Math.Round(minSpread * lot) != benefit)
            {
                return minSpread + 1;
            }
            else
            {
                return minSpread;
            }
        }

        /// <summary>
        /// ある損失を得ないための最大の差額を取得する
        /// </summary>
        /// <param name="loss">損失（円）</param>
        /// <returns></returns>
        static int GetLossMaxSpread(double lot, int loss)
        {
            //// bitflyer では端数は四捨五入（最近接偶数への丸め）されるようなので、
            //// 1 円の利益を得るためには、差額×ロットが 0.51 円以上でなければならない
            //// ※0.50円だと「最近接偶数への丸め」により、四捨五入すると0になってしまう
            //var maxSpread = (int)Math.Ceiling((0.5 + loss - 1) / lot);
            //// ここで「最近接偶数への丸め」を行い、期待値の損失になるようであれば-1して、「最近接偶数への丸め」でも指定した損失を被らないようにする
            //if (Math.Round(maxSpread * lot) == loss)
            //{
            //    return maxSpread - 1;
            //}
            //else
            //{
            //    return maxSpread;
            //}

            // きちんと計算すれば上だが、別にこれでも良い
            return GetProfitMinSpread(lot, loss) - 1;
        }

        /// <summary>
        /// ある金額で得られる利益と同じ利益が得られる最小の金額を取得
        /// </summary>
        /// <param name="lot"></param>
        /// <param name="benefit"></param>
        /// <returns></returns>
        static int GetMinPriceToObtainSameProfit(double lot, int price)
        {
            for (int i = 1; ; i++)
            {
                if (price < GetProfitMinSpread(lot, i + 1))
                {
                    return GetProfitMinSpread(lot, i);
                }
            }
        }

        /// <summary>
        /// ある金額で被る損失と同じ損失を被る最大の金額を取得
        /// </summary>
        /// <param name="lot"></param>
        /// <param name="benefit"></param>
        /// <returns></returns>
        static int GetMaxPriceToObtainSameLoss(double lot, int price)
        {
            for (int i = 1; ; i++)
            {
                if (price < GetProfitMinSpread(lot, i + 1))
                {
                    return GetLossMaxSpread(lot, i + 1);
                }
            }
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
