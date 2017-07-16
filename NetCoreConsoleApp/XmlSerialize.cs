using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace NetCoreConsoleApp
{
    public class XmlSerialize
    {
        public string ValueA = "ValueA";
        [XmlIgnore]
        public int ValueB = 0;

        private int _ValueC;
        public int ValueC
        {
            get { return _ValueC; }
            set { _ValueC = value; }
        }

        /// <summary>
        /// 自分自身をシリアライズする
        /// </summary>
        public void SerializeMySelf()
        {
            var xmlSerializer = new XmlSerializer(typeof(XmlSerialize));
            using (var fileStream = new FileStream("Output.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, this);
            }
        }
    }
}
