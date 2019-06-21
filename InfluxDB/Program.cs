using System;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb.Models;
using System.Collections.Generic;

namespace InfluxDB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var randam = new Random();

            var influxDbClient = new InfluxDbClient("http://localhost:8086/", "root", "root", InfluxDbVersion.Latest);

            // データベースの新規作成
            var dbName = "TestDb";
            var response = await influxDbClient.Database.CreateDatabaseAsync(dbName);

            // とりあえず適当なデータを１日分書き込む
            var dataTimeBase = DateTime.Today.AddDays(-1);// データの書き込み開始日時（昨日からにする）
            var interval = 30;// 30秒ごとにデータを記録する
            var count = 24 * 60 * 60 / interval;// 1日分の書き込み回数
            var lastProc = 0;
            var proc = 0;
            for (int i = 0; i < count; i++)
            {
                await influxDbClient.Client.WriteAsync(CreateCpuPoint(1, i), dbName);
                await influxDbClient.Client.WriteAsync(CreateCpuPoint(2, i), dbName);
                await influxDbClient.Client.WriteAsync(CreateNetworkPoint(1, i), dbName);
                await influxDbClient.Client.WriteAsync(CreateNetworkPoint(2, i), dbName);

                // 書き込む量が多いので、進捗率を表示する
                proc = i * 100 / count;
                if (proc != lastProc)
                {
                    Console.WriteLine($"{proc}% ({i}/{count})");
                    lastProc = proc;
                }
            }


            //// データの読み込み
            //var queries = new[]
            //{
            //    "SELECT * FROM reading WHERE time > now() - 1h",
            //    "SELECT * FROM reading WHERE time > now() - 2h"
            //};
            //var res2 = await influxDbClient.Client.QueryAsync(queries, dbName);
            ////var series = await influxDbClient.Serie.GetSeriesAsync(dbName);

            Console.WriteLine("Completed");
            Console.ReadLine();


            // CPU のセンサー情報を作成
            Point CreateCpuPoint(int id, int index)
            {
                return new Point()
                {
                    // 所属する Measurement の名前
                    Name = "CPU",
                    Tags = new Dictionary<string, object>()
                        {
                            { "CpuId", id },
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "Used", randam.Next(0, 100) },// CPU 使用率
                            { "Temperature", randam.Next(10, 50) },// 温度
                        },
                    // タイムスタンプを指定しなかった場合はサーバー側のタイムスタンプで記録される
                    Timestamp = dataTimeBase.AddSeconds(index * interval),// 時間をずらして記録
                };
            }

            // ネットワーク使用量を作成
            Point CreateNetworkPoint(int id, int index)
            {
                return new Point()
                {
                    // 所属する Measurement の名前
                    Name = "Network",
                    Tags = new Dictionary<string, object>()
                        {
                            { "EthernetId", id },
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "Send", randam.Next(0, 10000) },// 送信量
                            { "Recv", randam.Next(0, 10000) },// 受信量
                        },
                    // タイムスタンプを指定しなかった場合はサーバー側のタイムスタンプで記録される
                    Timestamp = dataTimeBase.AddSeconds(index * interval),// 時間をずらして記録
                };
            }
        }
    }
}
