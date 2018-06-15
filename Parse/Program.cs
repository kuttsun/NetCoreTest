using System;
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
    class Program
    {
        static void Main(string[] args)
        {
            // ファイルから読み込み
            var person = PersonJson.ReadFile("test.json");

            Serialize();
            Deserialize();

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
                ""Neet"": true
            }
            ";

            var person = JsonConvert.DeserializeObject<PersonJson>(json);
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.Gender);
            Console.WriteLine(person.Neet);
        }
    }
}
