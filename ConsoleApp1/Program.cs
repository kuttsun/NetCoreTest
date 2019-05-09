using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var list = new List<string>() { "Tokyo", "Osaka", "Yokohama", "Nagoya", "Kobe", "Tokyo", "Yokohama", "Sapporo", "Fukuoka", "Tokyo" };
            ////var list = new List<int>() { 1, 2, 2, 3, 3 };
            ////var list = new string[];

            ////var target = 0;
            ////var target = "Hoge";// 見つからなかった
            ////var target = "Kobe";// １つだけ見つかった
            //var target = "Tokyo";// 複数見つかった
            //try
            //{
            //    Console.WriteLine(list.First(x => x == target));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"First で例外 {ex.GetType()}");
            //}
            //try
            //{
            //    Console.WriteLine(list.FirstOrDefault(x => x == target));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"FirstOrDefault で例外 {ex.GetType()}");
            //}
            //try
            //{
            //    Console.WriteLine(list.Single(x => x == target));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Single で例外 {ex.GetType()}");
            //}
            //try
            //{
            //    Console.WriteLine(list.SingleOrDefault(x => x == target));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"SingleOrDefault で例外 {ex.GetType()}");
            //}

            GroupBy();

            Console.ReadKey();
        }

        static void GroupBy()
        {
            // "Japan" と "America"が重複しているリスト
            var countries = new List<string>() { "Japan", "Japan", "Japan", "America", "America", "China" };

            var groupedCountries = countries.GroupBy(x => x);
            //.Where(name => name.Count() > 1)
            //.Select(group => group.Key).ToList();

            // グループのキーを表示
            Console.WriteLine(string.Join(", ", groupedCountries.Select(x => x.Key)));

            // 重複チェック
            var keys = countries.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key);
            Console.WriteLine("重複しているデータ: " + string.Join(", ", keys));

            keys = countries.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key);
            Console.WriteLine("重複していないデータ: " + string.Join(", ", keys));
            //// 各グループのキーと個数を表示
            //foreach (var groupedCountry in groupedCountries)
            //{
            //    Console.WriteLine($"Key:{groupedCountry.Key}, Count:{groupedCountry.Count()}");

            //    // 各グループの中身を表示
            //    foreach (var country in groupedCountry)
            //    {
            //        Console.WriteLine(country);
            //    }
            //}

            //var persons = new List<Person>
            //{
            //    new Person(){ Name = "Hoge", Age = 20 },
            //    new Person(){ Name = "Piyo", Age = 20 },
            //    new Person(){ Name = "Foo",  Age = 30 },
            //    new Person(){ Name = "Bar",  Age = 30 },
            //};

            //// Age でグループ化
            //var groupedPersons = persons.GroupBy(x => x.Age);

            //foreach (var groupedPerson in groupedPersons)
            //{
            //    Console.WriteLine($"Key:{groupedPerson.Key}, Count:{groupedPerson.Count()}");

            //    // 各グループの中身を表示
            //    foreach (var person in groupedPerson)
            //    {
            //        Console.WriteLine($"Name:{person.Name}, Age:{person.Age}");
            //    }
            //}
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        static void JsonTupleTest()
        {
            var SampleDataList = new Dictionary<Tuple<int, string>, string>()
            {
                [Tuple.Create(0, "Foo")] = "sample1",
                [Tuple.Create(0, "Bar")] = "sample2",
                [Tuple.Create(1, "Foo")] = "sample3",
                [Tuple.Create(2, "Bar")] = "sample4",
            };

            var serializedData = Serialize(SampleDataList);

            Console.WriteLine(serializedData);


            //var deserializedData = Deserialize<Dictionary<Tuple<int, string>, string>>(serializedData);
            var deserializedData = JsonConvert.DeserializeObject<Dictionary<Tuple<int, string>, string>>(serializedData);

            Console.WriteLine(deserializedData);
        }

        static string Serialize<T>(T value)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var serializedData = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, value);
                serializedData = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return serializedData;
        }

        static T Deserialize<T>(string value)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var deserializedData = default(T);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                deserializedData = (T)serializer.ReadObject(stream);
            }
            return deserializedData;
        }

        //static void Main(string[] args)
        //{
        //    //BitFlyer.Spread();

        //    // 四捨五入のテスト
        //    //var target = 0.041936184;
        //    //var hoge = Math.Round(target, 8, MidpointRounding.AwayFromZero);
        //    //Console.WriteLine($"{target}\n{hoge}");

        //    // 排他処理のテスト
        //    //var lockTest = new LockTest();
        //    //Parallel.For(0, 99, i =>
        //    //{
        //    //    lockTest.Hoge(i);
        //    //});
        //    //Console.WriteLine("完了");
        //    //Console.ReadKey();

        //    //var discord = new Discord("Webhookurl");
        //    //discord.Send("test");

        //    //Person person = null;
        //    ////Console.WriteLine($"Name:{person?.Name}");


        //    //int age = (int)person?.Age;

        //    // TimeSpan には上限を超えた値も入力可能（90秒とか）
        //    var second = 15;// 60 * 60 * 24 * 7;
        //    var ts = new TimeSpan(0, 0, second);
        //    Console.WriteLine(ts.TotalSeconds);

        //    //var begin = new DateTime(2019, 12, 31, 12, 00, 00);
        //    //var end = begin.AddYears(1);
        //    //var current = begin;
        //    //while (current < end)
        //    //{
        //    //    var diff = current - DateTime.MinValue;
        //    //    if (diff.TotalSeconds % second == 0)
        //    //    {
        //    //        Console.WriteLine(current);
        //    //    }
        //    //    current = current.AddSeconds(1);
        //    //}

        //    var begin = new DateTime(2019, 12, 31, 12, 00, 00);
        //    var end = begin.AddMinutes(1);
        //    // ターゲットの用意
        //    var targets = new List<DateTime>();
        //    var current = begin;
        //    while (current < end)
        //    {
        //        targets.Add(current);
        //        current = current.AddSeconds(1);
        //    }
        //    // 確認
        //    foreach (var target in targets) Console.WriteLine(target);
        //    // 5秒ごとにまとめる
        //    var terms = new List<DateTime>();
        //    foreach (var target in targets)
        //    {
        //        var diff = target - DateTime.MinValue;
        //        if (diff.TotalSeconds % second == 0)
        //        {
        //            // まとめた結果を表示
        //            if (terms.Any())
        //            {
        //                Console.WriteLine("----------");
        //                foreach (var term in terms) Console.WriteLine(term);
        //            }
        //            // 新たな区間の開始
        //            terms.Clear();
        //        }
        //        terms.Add(target);
        //    }
        //    // 作成途中の区間がある
        //    if(terms.Any())
        //    {
        //        Console.WriteLine("-----current-----");
        //        foreach (var term in terms) Console.WriteLine(term);
        //    }


        //    Console.ReadKey();
        //}
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
