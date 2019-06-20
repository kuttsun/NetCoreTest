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
            var dbName = "TestDb";

            var influxDbClient = new InfluxDbClient("http://localhost:8086/", "root", "root", InfluxDbVersion.Latest);

            // データベースの新規作成
            var response = await influxDbClient.Database.CreateDatabaseAsync(dbName);

            // データの書き込み
            var point = new Point()
            {
                // 所属する Measurement の名前
                Name = "reading",
                Tags = new Dictionary<string, object>()
                {
                    { "SensorId", 8 },
                    { "SerialNumber", "00AF123B" }
                },
                Fields = new Dictionary<string, object>()
                {
                    { "Open", randam.Next(200, 300) },
                    { "High", randam.Next(300, 400) },
                    { "Low", randam.Next(100, 200) },
                    { "Close", randam.Next(200, 300) }
                },
                // タイムスタンプを指定しなかった場合はサーバー側のタイムスタンプで記録される
                //Timestamp = DateTime.UtcNow
            };



            await WritePoint("BitFlyer");
            await WritePoint("BitMex");


            // データの読み込み
            var queries = new[]
            {
                "SELECT * FROM reading WHERE time > now() - 1h",
                "SELECT * FROM reading WHERE time > now() - 2h"
            };
            var res2 = await influxDbClient.Client.QueryAsync(queries, dbName);
            //var series = await influxDbClient.Serie.GetSeriesAsync(dbName);


            Console.WriteLine("Hello World!");


            Point CreatePoint(string name, int index)
            {
                return new Point()
                {
                    // 所属する Measurement の名前
                    Name = name,
                    Tags = new Dictionary<string, object>()
                        {
                            { "SensorId", 8 },
                            { "SerialNumber", "00AF123B" }
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "Open", randam.Next(200, 300) },
                            { "High", randam.Next(300, 400) },
                            { "Low", randam.Next(100, 200) },
                            { "Close", randam.Next(200, 300) }
                        },
                    // タイムスタンプを指定しなかった場合はサーバー側のタイムスタンプで記録される
                    Timestamp = DateTime.UtcNow.AddSeconds(index),
                };
            }

            async Task WritePoint(string name)
            {
                // とりあえず適当な数書き込む
                for (int i = 0; i < 10; i++)
                {
                    await influxDbClient.Client.WriteAsync(CreatePoint(name, i), dbName);
                }
            }
        }
    }
}
