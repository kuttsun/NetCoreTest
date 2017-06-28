using System;
using System.Text;
using System.IO;
using System.Xml;


namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 文字化け対策 http://kagasu.hatenablog.com/entry/2016/12/07/004813
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine("Hello World!");
            XPathTest();
        }

        /// <summary>
        /// XPath のテスト
        /// </summary>
        static void XPathTest()
        {
            string str = @"
    <doc> 
        <head> 
            <title>タイトル</title> 
        </head> 
        <body> 
            <line>一行目</line> 
            <line>二行目</line> 
            <line>三行目</line> 
        </body> 
    </doc>";

            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(str));

            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            // タイトル表示
            Console.WriteLine(root.SelectSingleNode("head/title").InnerText);

            // 行選択
            nodeList = root.SelectNodes("body/line");

            foreach (XmlNode nd in nodeList)
            {
                // 行表示
                Console.WriteLine(nd.InnerText);
            }
        }
    }
}