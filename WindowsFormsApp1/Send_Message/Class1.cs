using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Send_Message
{
    public class Class1
    {
        List<string> ip = new List<string>();
        #region 发送客户端手机缴费信息
        /// <summary>
        /// 发送客户端手机缴费信息
        /// </summary>
        public void Send_Client_MobileChargeInfo(string CarCode, DateTime InTime, DateTime LastOutTime)
        {
            string chargeMessage = CarCode + "&" + InTime.ToString() + "&" + LastOutTime;
            List<byte> listAllByte = Get_RequestMessage("2", chargeMessage);
            foreach (string ip in ip)
            {
                try
                {
                    IPAddress.Parse(ip);

                    ClientRequestInfo ClientRequestInfo = new ClientRequestInfo();
                    ClientRequestInfo.IpAddress = ip;
                    ClientRequestInfo.sendbuffer = listAllByte.ToArray();
                    //发送消息
                    Thread t = new Thread(new ParameterizedThreadStart(Send_ClientRequest));
                    t.IsBackground = true;
                    t.Start(ClientRequestInfo);
                }
                catch { }
            }
            //return myResponse;
        }
        #endregion
        #region 获取发送请求信息
        /// <summary>
        /// 获取发送请求信息
        /// </summary>
        /// <param name="RequestType"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public List<byte> Get_RequestMessage(string RequestType, string Message)
        {
            return Get_RequestMessage(RequestType, Message, 3);
        }
        /// <summary>
        /// 新获取发送请求信息(数据长度5)
        /// </summary>
        /// <param name="RequestType"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public List<byte> Get_NewRequestMessage(string RequestType, string Message)
        {
            return Get_RequestMessage(RequestType, Message, 5);
        }
        /// <summary>
        /// 获取发送请求信息
        /// </summary>
        /// <param name="RequestType"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public List<byte> Get_RequestMessage(string RequestType, string Message, int dataLength)
        {
            List<byte> listAllByte = new List<byte>();

            //请求类型
            listAllByte.Add(byte.Parse(RequestType));

            if (Message != string.Empty)
            {
                byte[] byteRequest = Encoding.UTF8.GetBytes(Message.ToString());
                //数据长度
                string ImgLenth = byteRequest.Length.ToString().PadLeft(dataLength, '0');
                byte[] byteImgLenth = Encoding.UTF8.GetBytes(ImgLenth);
                for (int i = 0; i < byteImgLenth.Length; i++)
                {
                    listAllByte.Add(byteImgLenth[i]);
                }
                //请求内容
                for (int i = 0; i < byteRequest.Length; i++)
                {
                    listAllByte.Add(byteRequest[i]);
                }
            }
            else
            {
                string dataLengthTemp = "0".PadLeft(dataLength, '0');
                listAllByte.AddRange(Encoding.UTF8.GetBytes(dataLengthTemp));
            }

            // 结束标记
            string strSendEnd = "<Data_EOF>";
            byte[] byteSendEnd = Encoding.UTF8.GetBytes(strSendEnd);
            for (int i = 0; i < byteSendEnd.Length; i++)
            {
                listAllByte.Add(byteSendEnd[i]);
            }
            return listAllByte;
        }
        #endregion

        #region 发送请求
        /// <summary>
        /// 失败重发3次。客户端有定时检测，3秒钟连不上数据库会退出
        /// </summary>
        /// <param name="obj"></param>
        private void Send_ClientRequest(object obj)
        {
            for (int i = 0; i < 4; i++)
            {
                bool IsSuccess = Send_ClientRequestTcp(obj);
                if (IsSuccess) break;
                Thread.Sleep(1000);

            }
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="IpAddress">ip地址</param>
        /// <param name="sendbuffer">发送内容</param>
        /// <returns></returns>
        private bool Send_ClientRequestTcp(object obj)
        {
            //msg = "";
            ClientRequestInfo ClientRequestInfo = obj as ClientRequestInfo;
            //string IpAddress, byte[] sendbuffer
            //State = -1;
            bool IsSuccess = false;
            Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPAddress ip = IPAddress.Parse(ClientRequestInfo.IpAddress);
                IPEndPoint ipe = new IPEndPoint(ip,8080);

                //无返回超时问题
                SocketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10 * 1000);

                SocketClient.Connect(ipe);
                if (SocketClient.Connected)
                {
                    SocketClient.Send(ClientRequestInfo.sendbuffer, ClientRequestInfo.sendbuffer.Length, 0);
                    if (SocketClient.Connected)
                    {
                        /*图片协议格式 请求类型（1） 图片名称长度(3) 图片名称（200） 图片内容（不定长）结束标识(10)*/
                        /*请求协议格式 请求类型（1） 内容长度  内容（不定长）结束标识(10)*/
                        byte[] receivebuffer = new byte[64 * 1024];//接收数据
                        List<byte> list_receivebuffer = new List<byte>();
                        string content = string.Empty;
                        bool Get_Image = false;
                        while (true)
                        {
                            if (SocketClient == null) break;
                            int ReadBytes = SocketClient.Receive(receivebuffer, receivebuffer.Length, 0);
                            if (ReadBytes == 0)
                            {
                                break;
                            }
                            else
                            {
                                for (int i = 0; i < ReadBytes; i++)
                                {
                                    list_receivebuffer.Add(receivebuffer[i]);
                                }
                                content += Encoding.UTF8.GetString(list_receivebuffer.ToArray());
                                if (content.IndexOf("<Data_EOF>") > -1)
                                {
                                    SocketClient.Send(Encoding.UTF8.GetBytes("<OK_EOF>"));
                                    Get_Image = true;
                                    break;
                                }
                                if (content.IndexOf("<OK_EOF>") > -1)
                                {
                                    break;
                                }
                            }
                        }
                        IsSuccess = true;
                        if (!Get_Image)
                        {
                            switch (list_receivebuffer[0])
                            {
                     
                                case 105://心跳，获取连接字符串
                                    byte[] byteEof = Encoding.UTF8.GetBytes("<OK_EOF>");
                                    //msg = Encoding.UTF8.GetString(receivebuffer, 1, list_receivebuffer.ToArray().Length - 1 - byteEof.Length);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //远端信息 
                        //EndPoint tempRemoteEP = SocketClient.RemoteEndPoint;
                        //IPEndPoint tempRemoteIP = (IPEndPoint)tempRemoteEP;
                        //string rempip = tempRemoteIP.Address.ToString();
                        //string remoport = tempRemoteIP.Port.ToString();

                    }
                }
                else
                {
                }
            }
            catch (SocketException ex)
            {
                IsSuccess = false;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
            }
            finally
            {
                if (SocketClient != null && SocketClient.Connected)
                {
                    SocketClient.Shutdown(SocketShutdown.Both);
                    SocketClient.Dispose();
                    SocketClient = null;
                }
            }
            return IsSuccess;
        }
        #endregion
    }
    public class ClientRequestInfo
    {
        /// <summary>
        /// 客户端ip
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 发送信息
        /// </summary>
        public byte[] sendbuffer { get; set; }
    }

}
