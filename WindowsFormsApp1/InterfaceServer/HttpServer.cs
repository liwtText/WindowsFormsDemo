using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace InterfaceServer
{
    public class HttpServer
    {
        HttpListener httpListener;
        List<IWebService> lstWebRestfulServices;

        /// <summary>
        /// 是否正在监听
        /// </summary>
        public bool IsListening
        {
            get
            {
                return (httpListener != null && httpListener.IsListening);
            }
        }
        /// <summary>
        /// 添加监听服务
        /// </summary>
        /// <param name="Service"></param>
        public void AddService(IWebService Service)
        {
            if (lstWebRestfulServices == null)
                lstWebRestfulServices = new List<IWebService>();

            lstWebRestfulServices.Add(Service);
        }
        /// <summary>
        /// 批量添加监听服务
        /// </summary>
        /// <param name="lstService"></param>
        public void AddServiceRange(List<IWebService> lstService)
        {
            if (lstService != null && lstService.Count > 0)
            {
                foreach (IWebService item in lstService)
                    this.AddService(item);
            }
        }
        /// <summary>
        /// 启动监听
        /// </summary>
        public void Start()
        {
            try
            {
                //启动服务
                httpListener = new HttpListener();
                foreach (var item in lstWebRestfulServices)
                {
                    httpListener.Prefixes.Add(item.GetUrl());
                }
                httpListener.Start();
                httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), httpListener);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            try
            {
                if (httpListener != null)
                {
                    httpListener.Close();
                    httpListener = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 异步处理接收请求
        /// </summary>
        /// <param name="ar"></param>
        private void GetContextCallBack(IAsyncResult ar)
        {
            try
            {
                if (httpListener == null || !httpListener.IsListening)
                    return;
                httpListener = ar.AsyncState as HttpListener;
                HttpListenerContext context = httpListener.EndGetContext(ar);
                //再次监听请求
                httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), httpListener);
                //处理请求
                string ReturnData = Request(context.Request);
                //输出请求
                Response(context, ReturnData);
            }
            catch (Exception ex)
            {
                //ACS_Parking.Commons.LogFileCode.WriteErrorLogMessage("异步处理接收请求异常," + ex.ToString());
            }
        }
        /// <summary>
        /// 处理输入参数
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns></returns>
        private string Request(HttpListenerRequest request)
        {
            string retunStr = string.Empty;
            //POST请求处理
            string urlName = request.RawUrl.ToString();
            try
            {
                IWebService tempWebRestfulServices = null;

                IEnumerable<IWebService> templstWebRestfulServices = lstWebRestfulServices.Where(m =>
                {
                    Uri uri = new UriBuilder(m.GetUrl()).Uri;
                    return uri.LocalPath.TrimEnd('/') == request.Url.LocalPath.TrimEnd('/');
                });
                if (templstWebRestfulServices != null && templstWebRestfulServices.Count() > 0)
                {
                    tempWebRestfulServices = templstWebRestfulServices.ToList()[0];
                }
                else
                {
                    //ACS_Parking.Commons.LogFileCode.WriteLogMessage("停车服务接收数据：错误的服务路径" + request.Url.ToString());
                }

                if (tempWebRestfulServices != null)
                {
                    if (request.HttpMethod.ToLower().Equals("get"))
                    {
                        retunStr = "不支持GET请求";
                        //ACS_Parking.Commons.LogFileCode.WriteLogMessage("停车服务接收get数据：" + request.Url.ToString());
                    }
                    else if (request.HttpMethod.ToLower().Equals("post"))
                    {
                      
                        Stream SourceStream = request.InputStream;
                        byte[] currentChunk = ReadLineAsBytes(SourceStream);

                        if (currentChunk != null)
                        {
                            //获取数据中有空白符需要去掉，输出的就是post请求的参数字符串 如：username=linezero
                            string postJson = Encoding.UTF8.GetString(currentChunk).Replace("�", "");
                            //ACS_Parking.Commons.LogFileCode.WriteLogMessage(string.Format("停车服务路径:{0},接收post原始数据:{1}", urlName, postJson));
                            //返回
                            retunStr = tempWebRestfulServices.ProcessPost(postJson);
                        }
                    }
                }
                else
                {
                    //ACS_Parking.Commons.LogFileCode.WriteLogMessage(string.Format("停车服务路径:{0},未注册", urlName));
                }
            }
            catch (Exception ex)
            {
                //ACS_Parking.Commons.LogFileCode.WriteErrorLogMessage("处理停车服务请求失败," + urlName + ex.ToString());
            }
            finally
            {
                //if (!string.IsNullOrEmpty(retunStr)
                    //ACS_Parking.Commons.LogFileCode.WriteLogMessage(string.Format("停车服务路径:{0},返回数据:{1}", urlName, retunStr));
            }
            return retunStr;
        }
        /// <summary>
        /// 输出方法
        /// </summary>
        /// <param name="response">response对象</param>
        /// <param name="responseString">输出值</param>
        /// <param name="contenttype">输出类型默认为json</param>
        private static void Response(HttpListenerContext context, string responsestring)
        {
            try
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = context.Request.ContentType;
                context.Response.ContentEncoding = context.Request.ContentEncoding;
                byte[] buffer = context.Request.ContentEncoding.GetBytes(responsestring);
                //对客户端输出相应信息.
                context.Response.ContentLength64 = buffer.Length;
                System.IO.Stream output = context.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                //关闭输出流，释放相应资源
                output.Close();
            }
            catch (Exception ex)
            {
                //ACS_Parking.Commons.LogFileCode.WriteErrorLogMessage("返回请求失败," + ex.ToString());
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="SourceStream"></param>
        /// <returns></returns>
        private static byte[] ReadLineAsBytes(Stream SourceStream)
        {
            byte[] dataBytes = null;
            try
            {
                var resultStream = new MemoryStream();
                while (true)
                {
                    int data = SourceStream.ReadByte();
                    resultStream.WriteByte((byte)data);
                    if (data <= 0)
                        break;
                }
                resultStream.Position = 0;
                dataBytes = new byte[resultStream.Length];
                resultStream.Read(dataBytes, 0, dataBytes.Length);
            }
            catch (Exception ex)
            {
                //ACS_Parking.Commons.LogFileCode.WriteErrorLogMessage("读取请求失败," + ex.ToString());
            }
            return dataBytes;
        }
    }
}
