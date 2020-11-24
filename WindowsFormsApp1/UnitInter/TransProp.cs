using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitInter
{
    public class TransProp
    {
        /// <summary>
        /// 程序集
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 操作地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 是否转换(默认转换)
        /// </summary>
        public string IsTrans { get; set; }
        /// <summary>
        /// 头部
        /// </summary>
        public string Headers { get; set; }
        /// <summary>
        /// 请求超时时间
        /// </summary>
        public string TimeOut { get; set; }
        /// <summary>
        /// 包对象ID
        /// </summary>
        public string PackageID { get; set; }
        /// 接收数据格式列表
        /// </summary>
        public List<DataConfig> lstDataConfigReceive { get; set; }
        /// <summary>
        /// 发送数据格式列表
        /// </summary>
        public List<DataConfig> lstDataConfigSend { get; set; }
    }

    public class DataConfig
    {
        /// <summary>
        /// 本地名
        /// </summary>
        public string OldName { get; set; }
        /// <summary>
        /// 外部调用名
        /// </summary>
        public string NewName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string DataMode { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }

    public class PackageConfig
    {
        /// <summary>
        /// 包对象ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 程序集
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
    }
}
