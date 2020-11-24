using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp9
{
    [XmlRoot("ServiceDefination")]
    public class ServiceDefinationXml
    {
        /// <summary>
        /// 上行客户端
        /// </summary>
        [XmlArray("DeliverClientList")]
        public List<ClientItemXml> DeliverClients { get; set; }

        /// <summary>
        /// 下行服务端
        /// </summary>
        [XmlArray("ListenServerList")]
        public List<ServerItemXml> ListenServers { get; set; }
    }
    public abstract class BaseServiceItemXml
    {
        /// <summary>
        /// 服务描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
    }
    /// <summary>
    /// 上行客户端XML
    /// </summary>
    [XmlType("DeliverClient")]
    public class ClientItemXml : BaseServiceItemXml
    {
        /// <summary>
        /// 第一次启动时间
        /// </summary>
        public int DueTime { get; set; }

        /// <summary>
        /// 轮询时间间隔
        /// </summary>
        public int Interval { get; set; }
    }
    /// <summary>
    /// 下行服务端XML
    /// </summary>
    [XmlType("ListenServer")]
    public class ServerItemXml : BaseServiceItemXml
    {

    }
}
