using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace WindowsService1
{
    public class Config
    {
        #region
        /// <summary>
        /// 客户端识别图片路径
        /// </summary>
        public static string ClientPhotoPath = string.Empty;
        /// <summary>
        /// 服务端识别图片路径
        /// </summary>
        public static string ServerPhotoPath = string.Empty;
        /// <summary>
        /// 中央缴费识别图片
        /// </summary>
        public static string CentrePhotoPath = string.Empty;
        /// <summary>
        /// 客户端日志路径
        /// </summary>
        public static string ClientLogPath = string.Empty;
        /// <summary>
        /// 服务端日志路径
        /// </summary>
        public static string ServerLogPath = string.Empty;
        /// <summary>
        /// 中央缴费端日志路径
        /// </summary>
        public static string CentreLogPath = string.Empty;
        /// <summary>
        /// 对外接口端日志路径
        /// </summary>
        public static string InterFaceLogPath = string.Empty;
        /// <summary>
        /// 云端上传服务日志路径
        /// </summary>
        public static string PlugLogPath = string.Empty;
        ///// <summary>
        ///// 是否默认客户端识别图片路径
        ///// </summary>
        //public static bool IsDefaultClientPhotoPath = true;
        ///// <summary>
        ///// 是否默认服务端识别图片路径
        ///// </summary>
        //public static bool IsDefaultServerPhotoPath = true;
        ///// <summary>
        ///// 是否默认中央缴费识别图片路径
        ///// </summary>
        //public static bool IsDefaultCentrePhotoPath = true;
        ///// <summary>
        ///// 是否默认客户端日志路径
        ///// </summary>
        //public static bool IsDefaultClientLogPath = true;
        ///// <summary>
        ///// 是否默认服务端日志路径
        ///// </summary>
        //public static bool IsDefaultServerLogPath = true;
        ///// <summary>
        ///// 是否默认中央缴费端日志路径
        ///// </summary>
        //public static bool IsDefaultCentreLogPath = true;
        ///// <summary>
        ///// 是否默认对外接口端日志路径
        ///// </summary>
        //public static bool IsDefaultInterFaceLogPath = true;
        ///// <summary>
        ///// 是否默认云端上传服务日志路径
        ///// </summary>
        //public static bool IsDefaultPlugLogPath = true;
        #endregion

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
            //str_value = xDoc.SelectSingleNode("//appSettings//add[?key='" + AppKey + "']").Attributes["value"].Value;
            str_value = xDoc.SelectSingleNode("//appSettings//add[@key='" + AppKey + "']").Attributes["value"].Value;

            return str_value;
        }
        #endregion

        #region  设置config文件信息
        /// <summary>
        /// 设置config文件信息
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>
        public static void SetConfiValue(string AppKey, string AppValue)
        {
            string str_path = System.Windows.Forms.Application.ExecutablePath + ".config";
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称 
            xDoc.Load(str_path);
            xDoc.SelectSingleNode("//appSettings//add[@key='" + AppKey + "']").Attributes["value"].Value = AppValue;
            xDoc.Save(str_path);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        #region 更新config值
        /// <summary>
        /// 更新config值
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
}
