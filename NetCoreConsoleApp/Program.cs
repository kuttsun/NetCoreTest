using System;
using System.Text;



namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 文字化け対策 http://kagasu.hatenablog.com/entry/2016/12/07/004813
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            XPath.Test();
        }


    }
}