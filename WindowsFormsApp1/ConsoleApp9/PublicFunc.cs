using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp9
{
    public class PublicFunc
    {
        /// <summary>
        /// 加载XML配置文件
        /// </summary>
        public static TXmlType XmlSerializeToObject<TXmlType>(string xmlFileName)
        {
            var xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileName);
            if (!File.Exists(xmlFilePath))
            {
                return default(TXmlType);
            }
            try
            {
                using (var fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    var xmldes = new XmlSerializer(typeof(TXmlType));
                    return (TXmlType)xmldes.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                return default(TXmlType);
            }
        }
    }
}
