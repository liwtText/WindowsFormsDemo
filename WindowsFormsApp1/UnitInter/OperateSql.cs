using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Threading;
using System.Data;

namespace UnitInter
{
    public class OperateSql
    {
        #region 检测数据库连接状态(阻塞)
        /// <summary>
        /// 检测数据库服务是否启动(阻塞)
        /// </summary>
        public static void Check_SqlServer_Connection(string connectionString, string strDAL)
        {
            int index = 0;
            if (strDAL == "ACS_Parking.MySQLDAL")
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                serverIsOpen(conn, index);
            }
            else if (strDAL == "ACS_Parking.SQLServerDAL")
            {
                SqlConnection conn = new SqlConnection(connectionString);
                serverIsOpen(conn, index);
            }

        }
        #endregion

        #region 重载执行不同的数据库操作
        /// <summary>
        /// 数据库连接是否打开
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="index"></param>
        private static void serverIsOpen(SqlConnection conn, int index)
        {
            while (true)
            {
                index++;
                try
                {
                    conn.Open();
                    break;
                }
                catch
                {
                    if (index == 1)
                    {
                        string message = "数据库服务还未启动...";
                        //ACS_Parking.Commons.LogFileCode.WriteLogMessage(DateTime.Now.ToString() + message);
                    }
                }
                Thread.Sleep(500);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// 数据库连接是否打开
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="index"></param>
        private static void serverIsOpen(MySqlConnection conn, int index)
        {
            while (true)
            {
                index++;
                try
                {
                    conn.Open();
                    break;
                }
                catch
                {
                    if (index == 1)
                    {
                        string message = "数据库服务还未启动...";
                        //ACS_Parking.Commons.LogFileCode.WriteLogMessage(DateTime.Now.ToString() + message);
                    }
                }
                Thread.Sleep(300);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion
    }
}
