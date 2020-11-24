using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Configuration;
using Unit;
using UnitInter;

namespace InterfaceServer
{
    public class InterfaceConfigInfo
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static XmlConfig xmlConfig;
        /// <summary>
        /// 服务集
        /// </summary>
        public static List<IWebService> lstWebService = new List<IWebService>();
        /// <summary>
        /// 停车系统上传至本地服务
        /// </summary>
        //public static LocalUpLoadExtendClass localUpLoadExtendClass = LocalUpLoadExtendConfigXml.LoadXml();

        public static int TimeUpLoadInterval;
        /// <summary>
        /// 加载配置参数
        /// </summary>
        public static void LoadConfig()
        {
            #region XmlConfig
            try
            {
                xmlConfig = new XmlSerializerWrapper<XmlConfig>().Entity;


                #region 加载Service
                if (xmlConfig.lstTransPropServer != null && xmlConfig.lstTransPropServer.Count > 0)
                {
                    foreach (var item in xmlConfig.lstTransPropServer)
                    {
                        try
                        {
                            Assembly asm = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + item.AssemblyName);
                            if (asm != null)
                            {
                                Type t = asm.GetType(item.Name);
                                if (t != null)
                                {
                                    IWebService tempWebService = Activator.CreateInstance(t, item, null) as IWebService;
                                    if (tempWebService != null)
                                    {
                                        lstWebService.Add(tempWebService);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //LogFileCode.WriteErrorLogMessage("接口服务加载配置参数异常" + ex.ToString());
            }
            #endregion

            try
            {
                TimeUpLoadInterval = int.Parse(Get_ConfigValue("TimeUpLoadInterval"));
            }
            catch
            {
                TimeUpLoadInterval = 5000;
            }
        }

        #region 获取config值
        /// <summary>
        /// 获取config值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <returns></returns>
        public static string Get_ConfigValue(string AppKey)
        {
            string str_value = string.Empty;
            string str_path = System.Windows.Forms.Application.ExecutablePath + ".config";
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称 
            xDoc.Load(str_path);
            str_value = xDoc.SelectSingleNode("//appSettings//add[@key='" + AppKey + "']").Attributes["value"].Value;
            return str_value;
        }
        #endregion

        #region 设置config值
        /// <summary>
        /// 设置config值
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        private static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }

    public class XmlConfig
    {
        /// <summary>
        /// 作为服务器数据
        /// </summary>
        public List<TransProp> lstTransPropServer { get; set; }
        /// <summary>
        /// 作为客户端数据
        /// </summary>
        public List<TransProp> lstTransPropClient { get; set; }
        /// <summary>
        /// 作为打包配置
        /// </summary>
        public List<PackageConfig> lstPackageConfig { get; set; }
    }

}
