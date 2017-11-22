using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Json
{
    enum Gender
    {
        Male,
        Female,
        Unknown
    }
    class Program
    {
        static void Main(string[] args)
        {
            Serialize();

            Deserialize();

            Console.ReadKey();
        }

        static void Serialize()
        {
            Console.WriteLine("シリアライズテスト");

            var person = new Person
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
                ""Neet"": true
            }
            ";

            var person = JsonConvert.DeserializeObject<Person>(json);
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.Gender);
            Console.WriteLine(person.Neet);
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            // enum を文字列で出力したい場合は StringEnumConverter を使用する
            [JsonConverter(typeof(StringEnumConverter))]
            public Gender Gender { get; set; } = Gender.Male;
            public bool Neet { get; set; }
        }
    }
}
