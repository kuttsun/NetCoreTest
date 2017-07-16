using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace NetCoreConsoleApp
{
    class XPath
    {
        /// <summary>
        /// XPath のテスト
        /// </summary>
        public static void Test()
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
