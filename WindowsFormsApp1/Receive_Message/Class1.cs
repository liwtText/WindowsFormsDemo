using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receive_Message
{
    public class Class1
    {
        #region 监听客户端请求
        /// <summary>
        /// 监听客户端请求tcp服务端
        /// </summary>
        public void StartListen_Client()
        {
            string ServiceIp = "192.168.10.130";
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8080);
            string str_service_message = string.Format("服务监听ip：{0},端口:{1}", ServiceIp, 8080);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                while (true)
                {
                    listener.Listen(100);
                    Socket soc = listener.Accept();
                    //将要执行的方法排入线程池队列
                    System.Threading.WaitCallback wait = new WaitCallback(DealInquire_Client);
                    ThreadPool.QueueUserWorkItem(wait, soc);
                    //  ThreadPool.QueueUserWorkItem(DealInquire_Client, soc);
                }
            }
            catch (Exception ex)
            {
                string message = "侦听客户端请求失败,服务监听ip：" + ServiceIp + " | " + ex.ToString();
            }
        }
        #endregion
        #region 接收客户端请求
        /// <summary>
        /// 接收客户端请求
        /// </summary>
        /// <param name="objSocket"></param>
        public void DealInquire_Client(object objSocket)
        {

            Socket ServiceSocket = (Socket)objSocket;              //传入的socket连接
            byte[] rbuffer = new byte[64 * 1024];        //接收数据缓冲区
            List<byte> bytesList = new List<byte>();    //存放所有接收到数据的集合
            string content = "";                        //请求字符串内容
            //bool IsUpLoadImageOver = false;
            string RequestName = string.Empty;
            try
            {
                #region 接收数据
                while (true)
                {
                    if (ServiceSocket == null) break;
                    int ReadBytes = ServiceSocket.Receive(rbuffer, rbuffer.Length, 0);
                    if (ReadBytes == 0)
                    {
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < ReadBytes; i++)
                        {
                            bytesList.Add(rbuffer[i]);
                        }

                        #region 处理3.0原有的处理逻辑
                        try
                        {
                            content += Encoding.UTF8.GetString(bytesList.ToArray());
                            /*图片协议格式 请求类型（1） 图片名称长度(3) 图片名称（200） 图片内容（不定长）结束标识(10)*/
                            /*请求协议格式 请求类型（1） 内容（不定长）   结束标识(10)*/
                            if (content.IndexOf("<Data_EOF>") > -1)  //3.0原有的处理逻辑
                            {
                                //string RequestName = string.Empty;
                                switch (bytesList[0])
                                {
                                    case 1://上传图片
                                        RequestName = "上传图片";
                                        //IsUpLoadImageOver = true;
                                        ServiceSocket.Send(Encoding.UTF8.GetBytes("<OK_EOF>"));
                                        //保存图片
                                        //Thread t = new Thread(new ParameterizedThreadStart(imgInfo.Save_Image));
                                        //t.IsBackground = true;
                                        //t.Start(bytesList.ToArray());
                                        break;
                                 
                                    case 106://免密支付
                                        RequestName = "免密支付";
                                        //List<byte>  listByte = GetNonSecretPay(bytesList.ToArray());
                                        //ServiceSocket.Send(listByte.ToArray());
                                        break;
                                }

                             
                                //远端信息 
                                EndPoint tempRemoteEP = ServiceSocket.RemoteEndPoint;
                                IPEndPoint tempRemoteIP = (IPEndPoint)tempRemoteEP;
                                string rempip = tempRemoteIP.Address.ToString();
                                string remoport = tempRemoteIP.Port.ToString();
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                        #endregion
                   
                    }
                }
                #endregion
            }
            //catch (SocketException e)
            //{
            //    ACS_Parking.Commons.LogFileCode.WriteLogMessage("服务端socket套接字错误," + e.Message);
            //}
            catch (Exception ex)
            {
            }
            finally
            {
                if (ServiceSocket != null && ServiceSocket.Connected)
                {
                    //ServiceSocket.Shutdown(SocketShutdown.Both);
                    ServiceSocket.Dispose();
                    ServiceSocket = null;
                }
            }
        }
    
        #endregion
        #region 发送返回信息时，加入发送数据长度
        /// <summary>
        /// 发送返回信息时，加入发送数据长度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ConnectByteArray(byte[] data)
        {
            //发送数据长度
            int new_length = Encoding.UTF8.GetByteCount(data.Length.ToString()) + data.Length;
            byte[] head = Encoding.UTF8.GetBytes(new_length.ToString());

            long total_length = head.Length + data.Length;
            byte[] returnByte = new byte[total_length];
            for (long i = 0; i < head.Length; i++)
            {
                returnByte[i] = head[i];
            }
            for (long j = head.Length; j < total_length; j++)
            {
                returnByte[j] = data[j - head.Length];
            }
            return returnByte;
        }
        #endregion
    }
}
