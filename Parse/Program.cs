using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parse
{
    enum Gender
    {
        Male,
        Female,
        Unknown
    }

    enum Birthplace
    {
        Japan
    }
    class Program
    {
        static void Main(string[] args)
        {

            //var setting = new JsonSerializerSettings()
            //{
            //    // enum 全てを文字列でシリアライズしたい場合は Converters を設定する
            //    Converters = new List<JsonConverter>(){
            //        new StringEnumConverter()
            //    },
            //    // インデントあり
            //    Formatting = Formatting.Indented
            //};

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                // enum 全てを文字列でシリアライズしたい場合は Converters を設定する
                Converters = new List<JsonConverter>(){
                    new StringEnumConverter()
                },
                // インデントあり
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(
                new Person
                {
                    Name = "hoge",
                    Age = 30,
                    Gender = Gender.Male,
                    Birthplace = Birthplace.Japan
                });
            Console.WriteLine(json);

            //var json = JsonConvert.SerializeObject(
            //    new Person
            //    {
            //        Name = "hoge",
            //        Age = 30,
            //        Gender = Gender.Male,
            //        Birthplace = Birthplace.Japan
            //    },
            //    Formatting.Indented);
            //Console.WriteLine(json);

            // ファイルから読み込み
            //var person = PersonJson.ReadFile("test.json");
            //Serialize();
            //Deserialize();

            Console.ReadKey();
        }

        static void Serialize()
        {
            Console.WriteLine("シリアライズテスト");

            var person = new PersonJson
            {
                Name = "hoge",
                Age = 30,
                Gender = Gender.Male,
                Neet = true
            };

            var json = JsonConvert.SerializeObject(person, Formatting.Indented);
            Console.WriteLine(json);
        }

        static void Deserialize()
        {
            Console.WriteLine("デシリアライズテスト");

            string json = @"
            {
                ""Name"": ""hoge"",
                ""Age"": 30,
                ""Gender"": ""Male"",
                ""Neet"": true,
                ""Birthday"": ""12:23:45"",
                ""Time"": ""01:23:45""
            }
            ";

            var person = JsonConvert.DeserializeObject<PersonJson>(json);
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.Gender);
            Console.WriteLine(person.Neet);
            Console.WriteLine(person.Birthday);// 時刻情報しかない場合はその日の日付が入る
            Console.WriteLine(person.Time);

            // 時刻の部分だけを取得する
            TimeSpan tsNow = person.Birthday.TimeOfDay;

            // 取得した時刻を表示する
            Console.WriteLine(tsNow.ToString());
        }
    }
}
