using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parse
{
    class PersonJson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        //  特定の enum プロパティを文字列でシリアライズしたい場合は StringEnumConverter を使用する
        //[JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; } = Gender.Male;
        public bool Neet { get; set; }
        public DateTime Birthday { get; set; }
        public TimeSpan Time { get; set; }

        public void WriteFile(string path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(json);
            }
        }

        public static PersonJson ReadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                return JsonConvert.DeserializeObject<PersonJson>(sr.ReadToEnd());
            }
        }

    }
}
