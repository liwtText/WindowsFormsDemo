using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private string ip1="192.168.10.103";//Ip地址
        private int port=8888;//监听端口号
        public Form1()
        {
            InitializeComponent();
        }
        Socket socketSend;
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //创建负责通信的Socket 
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ip = IPAddress.Parse(ip1);

                //创建一个IPEndPoint类的实例，用远程服务器的IP地址和端口号来初始化它 
                IPEndPoint point = new IPEndPoint(ip, port);

                //获得要连接的远程服务器应用程序的IP地址和端口号，并建立与远程服务器的连接 
                socketSend.Connect(point);

                ShowMsg("连接成功");


                //开启一个新的线程，不停的接收服务端发送过来的消息 
                Thread th = new Thread(GetData);
                th.IsBackground = true;  //IsBackground获取或设置一个值，该值指示某个线程是否为后台线程。 
                th.Start();

            }
            catch
            {

            }
        }
        void ShowMsg(string str)
        {
            //在程序加载的时候取消跨线程的检查 
            richTextBox1.AppendText("\r\n" + str + "\r\n");

        }


        /// <summary>   
        /// 客户端不停的接收服务端发送过来的消息   
        /// </summary>   
        /// <param name="o"></param>   
        void GetData(object o)
        {

            while (true)
            {
                try
                {
                    //将服务端发过来的数据先放到一个字节数组里面去   
                    byte[] buffer = new byte[1024 * 1024 * 2]; //创建一个字节数组，字节数组的长度为2M   

                    //实际接收到的有效字节数; (利用Receive方法接收客户端传过来的数据，然后把数据保存到buffer字节数组中，返回一个接收到的数据的长度)   
                    int r = socketSend.Receive(buffer);

                    if (r == 0) //如果接收到的有效字节数为0 说明客户端已经关闭了。这时候就跳出循环了。   
                    {
                        //只有客户端给用户发消息，不可能是发0个长度的字节。即便发空消息，空消息也是有过个长度的。所有接收到的有效字节长度为0就代表客户端已经关闭了   
                        break;
                    }


                    //将buffer这个字节数组里面的数据按照UTF8的编码，解码成我们能够读懂的的string类型，因为buffer这个数组的实际存储数据的长度是r个 ，所以从索引为0的字节开始解码，总共解码r个字节长度。   
                    string str = Encoding.UTF8.GetString(buffer, 0, r);

                    //RemoteEndPoint方法是获取服务器的IP地址和端口号 
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + str);
                }
                catch
                {

                }
            }
        }


        /// <summary> 
        /// 客户端给服务器发送消息 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void btnSend_Click(object sender, EventArgs e)
        {
            //获取输入框输入的数据 
            string str = textBox1.Text.Trim();
            //将输入框的数据转换成二进制数据 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);

            //Send方法是将数据发送到连接的Socket 
            socketSend.Send(buffer);

        }

       
    }
}
