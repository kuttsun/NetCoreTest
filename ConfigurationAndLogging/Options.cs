using System;
using System.Collections.Generic;
using System.Text;


namespace ConfigurationAndLogging
{
    // オプションを保持するためのクラス
    public class MyOptions
    {
        public string Str { get; set; }
        public Account Account { get; set; }
    }

    public class Account
    {
        public List<User> Users { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class MyOptions2
    {
        public string Str { get; set; }
    }
}