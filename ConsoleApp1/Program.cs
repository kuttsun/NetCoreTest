using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // ConsoleApp2 は単独でも実行可能だが、
            // ConsoleApp1 からもメソッドをコールできる
            ConsoleApp2.Test.Output();

            Console.ReadKey();
        }
    }
}
