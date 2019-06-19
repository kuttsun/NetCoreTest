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
            var dbName = "TestDb";

            var influxDbClient = new InfluxDbClient("http://localhost:8086/", "root", "root", InfluxDbVersion.Latest);

            // データベースの新規作成
            var response = await influxDbClient.Database.CreateDatabaseAsync(dbName);

            // データの書き込み
            var pointToWrite = new Point()
            {
                Name = "reading", // serie/measurement/table to write into
                Tags = new Dictionary<string, object>()
                {
                    { "SensorId", 8 },
                    { "SerialNumber", "00AF123B" }
                },
                Fields = new Dictionary<string, object>()
                {
                    { "SensorState", "act" },
                    { "Humidity", 431 },
                    { "Temperature", 22.1 },
                    { "Resistance", 34957 }
                },
                Timestamp = DateTime.UtcNow // optional (can be set to any DateTime moment)
            };

            var res1 = await influxDbClient.Client.WriteAsync(pointToWrite, dbName);


            // データの読み込み
            var queries = new[]
            {
                "SELECT * FROM reading WHERE time > now() - 1h",
                "SELECT * FROM reading WHERE time > now() - 2h"
            };
            var res2 = await influxDbClient.Client.QueryAsync(queries, dbName);
            //var series = await influxDbClient.Serie.GetSeriesAsync(dbName);


            Console.WriteLine("Hello World!");
        }
    }
}
