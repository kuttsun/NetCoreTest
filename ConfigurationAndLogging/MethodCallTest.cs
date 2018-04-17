using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationAndLogging
{
    public class MethodCallTest
    {
        public static void Foo(string date, string level, string message)
        {
            Console.WriteLine($"MethodCallTest:{date} [{level}] {message}");
        }
    }
}
