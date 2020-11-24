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

namespace SocketService
{
    public partial class Form1 : Form
    {
        private IPAddress ip1;//Ip地址
        private int port = 8888;//监听端口号
        public Form1()
        {
            InitializeComponent();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            text();
        }

        private void text()
        {

            try
            {
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Any:提供一个 IP 地址，指示服务器应侦听所有网络接口上的客户端活动。此字段为只读。   
                IPAddress ip = IPAddress.Any;
                //IPAddress ip = IPAddress.Parse(ip1.ToString());

                //创建端口号对象；将txtPort.Text控件的值设为服务端的端口号   
                IPEndPoint point = new IPEndPoint(ip, port);

                //监听   
                socketWatch.Bind(point);
                ShowMsg("监听成功"); //注意：这个ShowMeg方法是自己定义的。看下面的代码可以找到这个方法 

                socketWatch.Listen(10);//连接队列的最大长度 ;即：一个时间点内最大能让几个客户端连接进来，超过长度就进行排队   

                //等待客户端连接;Accept()这个方法能接收客户端的连接，并为新连接创建一个负责通信的Socket   
                Thread th = new Thread(Listen); //被线程执行的方法如果有参数的话,参数必须是object类型   

                Control.CheckForIllegalCrossThreadCalls = false; //因为.net不允许跨线程访问的，所以这里取消跨线程的检查。.net不检查是否有跨线程访问了，所有就不会报： “从不是创建控件“txtLog”的线程访问它” 这个错误了，从而实现了跨线程访问   

                th.IsBackground = true; //将th这个线程设为后台线程。   

                //Start(object parameter); parameter:一个对象，包含线程执行的方法要使用的数据,即线程执行Listen方法，Listen的参数   
                th.Start(socketWatch);  //这个括号里的参数其实是Listen()方法的参数。因为Thread th = new Thread(Listen)这个括号里只能写方 法名(函数名) 但是Listen()方法是有参数的，所有就要用Start()方法将它的参数添加进来   
            }
            catch (Exception ex)
            {

                throw;
            }
           
            
        }
        //将远程连接的客户端的IP地址和Socket存入集合中 
        Dictionary<string, Socket> dic = new Dictionary<string, Socket>();


        /// <summary>   
        /// 等待客户端连接，如果监控到有客户端连接进来就创建一个与之通信的Socket   
        /// </summary>   
        /// <param name="o"></param>   

        Socket socketSend;  // 定义一个负责通信的Socket 
        void Listen(object o) //这里为什么不直接传递Socket类型的参数呢？ 原因是：被线程执行的方法如果有参数的话,参数必须是object类型   
        {
            Socket socketWatch = o as Socket;

            while (true) //为什么这里要有个while循环？因为当一个人连接进来的时候创建了与之通信的Socket后就程序就会往下执行了，就不会再回来为第二个人的连接创建负责通信的Socket了。（应该是每个人(每个客户端)创建一个与之通信的Socket）所以要写在循环里。   
            {
                try
                {
                    //等待客户端连接;Accept()这个方法能接收客户端的连接，并为新连接创建一个负责通信的Socket   
                    socketSend = socketWatch.Accept();

                    dic.Add(socketSend.RemoteEndPoint.ToString(), socketSend); //（根据客户端的IP地址和端口号找负责通信的Socket，每个客户端对应一个负责通信的Socket），ip地址及端口号作为键，将负责通信的Socket作为值填充到dic键值对中。   
                    comboBox1.Items.Add(socketSend.RemoteEndPoint.ToString());
                    //我们通过负责通信的这个socketSend对象的一个RemoteEndPoint属性，能够拿到远程连过来的客户端的Ip地址跟端口号   
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");//效果：192.168.1.32:连接成功   

                    //客户端连接成功后，服务器应该接收客户端发来的消息。    
                    Thread getdata = new Thread(GetData);
                    getdata.IsBackground = true;
                    getdata.Start(socketSend);
                }
                catch
                { }

            }

        }


        /// <summary>   
        /// 服务端不停的接收客户端发送过来的消息   
        /// </summary>   
        /// <param name="o"></param>   
        void GetData(object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                try
                {
                    //将客户端发过来的数据先放到一个字节数组里面去   
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


                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + str);
                }
                catch
                {

                }
            }
        }
        private void ShowMsg(string str)
        {
            richTextBox1.AppendText(str + "\r\n"); //将str这个字符串添加到txtLog这个文本框中。  
        }


        /// <summary> 
        /// 服务器给客户端发送消息 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void Button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null) //如果comboBox控件没有选中值。就提示用户选择客户端   
            {
                MessageBox.Show("请选择客户端");
                return;
            }
            //服务器要想给客户端发消息，就需要先拿到负责通信的那个Socket 

            string str = textBox1.Text.Trim();
            ShowMsg(str);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);

            string getIp = comboBox1.SelectedItem as string; //comboBox存储的是客户端的（ip+端口号） 
            socketSend = dic[getIp] as Socket; //根据这个（ip及端口号）去dic键值对中找对应 赋值与客户端通信的Socket【每个客户端都有一个负责与之通信的Socket】   
            socketSend.Send(buffer);
        }

    }
}
