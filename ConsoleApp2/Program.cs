using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main()
        {
            var json = @"{ ""status"":0,""data"":{ ""status"":""OPEN""},""responsetime"":""2019-06-04T23:14:03Z""}";
            var status = Deserialize<StatusData>(json);
            Console.WriteLine(status);
            Console.ReadKey();
        }

        static T Deserialize<T>(string json)
        {
            var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(json);
            return responseData.Data;
        }
    }

    public enum Status
    {
        Maintenance,
        Preopen,
        Open,
    }

    public class ResponseData<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string ResponseTime { get; set; }
    }

    public class StatusData
    {
        public Status Status { get; set; }
    }
}
