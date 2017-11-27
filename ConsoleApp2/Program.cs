using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConsoleApp2 は ConsoleApp1 から参照されているが、
            // ConsoleApp2 単独でも実行可能

            Console.WriteLine("ConsoleApp2 Hello World!");

            Test.Output();

            Console.ReadKey();
        }

        
    }

    public class Test
    {
        // このメソッドは ConsoleApp1 からコールされている
        public static void Output()
        {
            Console.WriteLine("ConsoleApp2");
        }
    }
}
