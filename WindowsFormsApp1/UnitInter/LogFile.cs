using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace UnitInter
{
    public class LogFile
    {
        public static string currentPath = "";

        static LogFile()
        {
            //log4netUtil.Log4netHelper.SetPathDir(currentPath);
            //log4netUtil.Log4netHelper.Init();
        }

        #region 写入日志文件
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="file_path">日志文件路径</param>
        /// <param name="message">写入的日志内容</param>
        public static void WriteLogMessage(string message)
        {
            try
            {
                //log4netUtil.Log4netHelper.GetInstance(string.Empty).Info(message);
            }
            catch
            {
            }
        }
        #endregion

        #region 写入错误日志
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="message">错误日志信息</param>
        public static void WriteErrorLogMessage(string message)
        {
            try
            {
                //log4netUtil.Log4netHelper.GetInstance(string.Empty).Error(message);
            }
            catch
            {
            }
        }
        #endregion

        #region 按日期删除日志
        /// <summary>
        /// 按日期删除日志
        /// </summary>
        public static void ClearLog(int days)
        {
            try
            {
                DirectoryInfo logDir = new DirectoryInfo(Path.Combine(currentPath, "LogFiles"));
                var dtNow = DateTime.Now;
                DateTime tmpDt;

                FileInfo[] files = logDir.GetFiles();
                foreach (var itm in files)
                {
                    try
                    {
                        if (itm.Name.Length < 8)
                            continue;
                        string tempFile = System.Text.RegularExpressions.Regex.Match(itm.Name, @"[\d]{8}").ToString();
                        if (!DateTime.TryParse(tempFile.Insert(4, "/").Insert(7, "/"), out tmpDt))
                            continue;
                        if (dtNow >= tmpDt.AddDays(days))
                        {
                            File.Delete(itm.FullName);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
