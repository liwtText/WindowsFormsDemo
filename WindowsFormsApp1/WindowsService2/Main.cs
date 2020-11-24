using InterfaceServer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService2
{
    public class Main
    {
        HttpServer server = null;
        /// <summary>
        /// 是否停止
        /// </summary>
        bool isStop = false;
        /// <summary>
        /// 按秒计时
        /// </summary>
        System.Timers.Timer TickTime;
        /// <summary>
        /// 定时发送数据
        /// </summary>
        System.Timers.Timer SendDataTime;
        /// <summary>
        /// 定时刷新收费规则
        /// </summary>
        System.Threading.Timer RefreshChargeRuleTimer;
        /// <summary>
        /// 服务启动
        /// </summary>
        public void Start()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    conn.Close();
                }

                InterfaceConfigInfo.LoadConfig();
                //加载手机配置参数
                try
                {
                    if (InterfaceConfigInfo.lstWebService != null && InterfaceConfigInfo.lstWebService.Count > 0)
                    {
                        server = new HttpServer();
                        server.AddServiceRange(InterfaceConfigInfo.lstWebService);
                        server.Start();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            try
            {
                isStop = true;
                if (server != null)
                    server.Stop();

            }
            catch (Exception ex)
            {
            }
        }
    }
}
