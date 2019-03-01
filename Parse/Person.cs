using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Parse
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        //  特定の enum プロパティを文字列でシリアライズしたい場合は StringEnumConverter を使用する
        //[JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public Birthplace Birthplace { get; set; }
    }
}
