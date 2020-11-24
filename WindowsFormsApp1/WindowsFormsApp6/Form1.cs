using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "select * from pb_pstation";
            getDataSetBySql(str);
        }

        public DataSet getDataSetBySql(String sql)
        {
            //安装版本和数据库相兼容的odbc驱动 mysql odbc下载地址https://dev.mysql.com/downloads/connector/odbc/
            try
            {
                DataSet ds = new DataSet();
                OdbcCommand command = new OdbcCommand(sql);  //command  对象
                String connstring = "DRIVER={MySQL ODBC 5.1 Driver};server=localhost;uid=root;password=lf0507;database=test";  //ODBC连接字符串
                using (OdbcConnection connection = new OdbcConnection(connstring))  //创建connection连接对象
                {
                    command.Connection = connection;
                    connection.Open();  //打开链接
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);  //实例化dataadapter
                    adapter.Fill(ds);  //填充查询结果
                    return ds;
                }
            }
            catch (Exception ex)
            {
               return null;
            }
        }
    }
}
