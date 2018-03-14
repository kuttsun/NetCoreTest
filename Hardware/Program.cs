using System;
using System.Management;

namespace Hardware
{
    class Program
    {
        static void Main(string[] args)
        {
            GetProcessor();
            Console.WriteLine("----------");
            GetTemperatureProbe();
            Console.ReadKey();
        }

        static void GetProcessor()
        {
            // WMI クラスを取得
            var mc = new ManagementClass("Win32_Processor");

            // CPU 一覧を取得
            var moc = mc.GetInstances();

            // CPU 情報を順に表示
            foreach (var mo in moc)
            {
                // インデクサーにプロパティ名を指定して情報を取得
                Console.WriteLine("DeviceID: " + mo["DeviceID"]);
                Console.WriteLine("Name: " + mo["Name"]);
                Console.WriteLine("MaxClockSpeed: " + mo["MaxClockSpeed"]);
                Console.WriteLine("L2CacheSize: " + mo["L2CacheSize"]);
            }
        }

        static void GetTemperatureProbe()
        {
            // WMI クラスを取得
            var mc = new ManagementClass("Win32_TemperatureProbe");

            var moc = mc.GetInstances();

            if (moc.Count == 0)
            {
                Console.WriteLine("Not Supported");
                return;
            }

            // CPU 情報を順に表示
            foreach (var mo in moc)
            {
                // インデクサーにプロパティ名を指定して情報を取得
                Console.WriteLine("DeviceID: " + mo["DeviceID"]);
                Console.WriteLine("CurrentReading: " + mo["CurrentReading"]);
                Console.WriteLine("StatusInfo: " + mo["StatusInfo"]);
            }
        }
    }
}
