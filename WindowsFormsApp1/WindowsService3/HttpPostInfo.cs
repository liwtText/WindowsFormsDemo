using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace WindowsService3
{
    public class HttpPostInfo
    {
        /// <summary>
        /// 访问地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string dataStr { get; set; }
        /// <summary>
        /// 获取或设置 System.Net.HttpWebRequest.GetResponse() 和 System.Net.HttpWebRequest.GetRequestStream()
        /// 请求超时前等待的毫秒数。默认值是 100,000 毫秒（100 秒）。
        /// </summary>
        public int timeOut { get; set; }
        /// <summary>
        /// 获取或设置写入或读取流时的超时（以毫秒为单位）。
        /// 在写入超时或读取超时之前的毫秒数。默认值为 300,000 毫秒（5 分钟）。
        /// </summary>
        public int RecviceWritetimeOut { get; set; }
        /// <summary>
        /// 构成 HTTP 标头的名称/值对的集合。
        /// </summary>
        public string headers { get; set; }
        /// <summary>
        ///  Content-typeHTTP 标头的值。
        /// </summary>
        public string ContentType { get; set; }

        public HttpPostInfo()
        {
        }
        public HttpPostInfo(string url, string dataStr, int timeOut, int RecviceWritetimeOut, string headers, string ContentType)
        {
            this.url = url;
            this.dataStr = dataStr;
            this.timeOut = timeOut;
            this.RecviceWritetimeOut = RecviceWritetimeOut;
            this.headers = headers;
            this.ContentType = ContentType;
        }

        /// <summary>
        /// 上传基础数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        public static string Post(string url, string dataStr)
        {
            return Post(url, dataStr, 0, "");
        }
        /// <summary>
        /// 上传基础数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dataStr">数据</param>
        /// <param name="timeOut">毫秒</param>
        /// <param name="headers">头(key:valuekey:value)</param>
        /// <returns></returns>
        public static string Post(string url, string dataStr, int timeOut, string headers)
        {
            return Post(url, dataStr, timeOut, timeOut, headers);
        }
        /// <summary>
        /// 上传基础数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dataStr">数据</param>
        /// <param name="timeOut">毫秒</param>
        /// <param name="RecviceWritetimeOut">毫秒</param>
        /// <param name="headers">头(key:valuekey:value)</param>
        /// <returns></returns>
        public static string Post(string url, string dataStr, int timeOut, int RecviceWritetimeOut, string headers)
        {
            return Post(url, dataStr, timeOut, RecviceWritetimeOut, headers, "application/json;charset=utf-8");
        }
        /// <summary>
        /// 上传基础数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dataStr">数据</param>
        /// <param name="headers">头(key:valuekey:value)</param>
        /// <param name="ContentType">Content-typeHTTP 标头的值</param>
        /// <returns></returns>
        public static string Post(string url, string dataStr, string headers, string ContentType)
        {
            return Post(url, dataStr, 0, 0, headers, ContentType);
        }
        /// <summary>
        /// 上传基础数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dataStr">数据</param>
        /// <param name="timeOut">毫秒</param>
        /// <param name="RecviceWritetimeOut">毫秒</param>
        /// <param name="headers">头(key:valuekey:value)</param>
        /// <param name="ContentType">Content-typeHTTP 标头的值</param>
        /// <returns></returns>
        public static string Post(string url, string dataStr, int timeOut, int RecviceWritetimeOut, string headers, string ContentType)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string retStr = "";//数据返回
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(dataStr);

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.Method = "POST";
            request.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json;charset=utf-8" : ContentType;
            request.ContentLength = data.Length;
            request.Proxy = null;
            if (timeOut > 0)
            {
                request.Timeout = timeOut;
            }
            if (RecviceWritetimeOut > 0)
            {
                request.ReadWriteTimeout = RecviceWritetimeOut;
            }
            if (!string.IsNullOrEmpty(headers))
            {
                request.Headers.Add(headers);
            }
            try
            {
                Stream sm = request.GetRequestStream();
                sm.Write(data, 0, data.Length);
                sm.Close();

                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {

                    retStr = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteErrorLogMessage("http POST失败," + ex.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return retStr;
        }
        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static HtmlAgilityPack.HtmlDocument Get(string url)
        {
            System.GC.Collect();

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(ValidateServerCertificate);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                ////设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();
                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //result = sr.ReadToEnd().Trim();
                doc.Load(sr);
                sr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return doc;
        }

        public static Stream Get1(string url)
        {
            System.GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(ValidateServerCertificate);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                ////设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();
                //获取HTTP返回数据
                //StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                //result = sr.ReadToEnd().Trim();

                stream = response.GetResponseStream();

                //stream.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //finally
            //{
            //    //关闭连接和流
            //    if (response != null)
            //    {
            //        response.Close();
            //    }
            //    if (request != null)
            //    {
            //        request.Abort();
            //    }
            //}
            return stream;
        }

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="filename">下载文件重命名</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool DownLoadFile(string url, string filename, out string msg)
        {
            System.GC.Collect();

            bool isOk = false;
            msg = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(ValidateServerCertificate);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                ////设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                var DownMD5 = response.Headers["md5"];

                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    //StreamReader sr = new StreamReader(response.GetResponseStream());
                    //string abc= sr.ReadToEnd();

                    BufferedStream sm = new BufferedStream(response.GetResponseStream());

                    byte[] buffer = new byte[1024];
                    int r = -1;
                    while ((r = sm.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, r);
                    }
                    sm.Close();
                }

                if (File.Exists(filename))
                {
                    string myMD5 = filename;//MD5Encrypt.GetFileMD5Hash(filename);
                    if (DownMD5 == myMD5)
                        isOk = true;
                    else
                        msg = "MD5校验失败,收到MD5:" + DownMD5 + ";本地计算MD5:" + myMD5;
                }
                else
                {
                    msg = "文件不存在：" + filename;
                }
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

            return isOk;
        }
        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="filename">下载文件重命名</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetFile(string url, string filename, out string msg)
        {
            System.GC.Collect();

            bool isOk = false;
            msg = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(ValidateServerCertificate);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                ////设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //创建相关目录
                Directory.CreateDirectory(Path.GetDirectoryName(filename));

                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    BufferedStream sm = new BufferedStream(response.GetResponseStream());

                    byte[] buffer = new byte[64 * 1024];
                    int r = -1;
                    while ((r = sm.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, r);
                    }
                    fs.Flush();
                    sm.Close();
                }

                isOk = File.Exists(filename);
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

            return isOk;
        }
        /// <summary>
        /// 验证证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        /// <summary>
        /// 读取url参数
        /// </summary>
        /// <param name="url">url字符串</param>
        /// <param name="baseUrl">baseUrl</param>
        /// <param name="nvc">参数列表</param>
        public static bool ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            nvc = new NameValueCollection();
            baseUrl = "";
            try
            {
                if (url == "")
                    return false;

                int questionMarkIndex = url.IndexOf('?');

                if (questionMarkIndex == -1)
                {
                    baseUrl = url;
                    return false;
                }
                baseUrl = url.Substring(0, questionMarkIndex);
                if (questionMarkIndex == url.Length - 1)
                    return false;
                string ps = url.Substring(questionMarkIndex + 1);

                // 开始分析参数对    
                Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                MatchCollection mc = re.Matches(ps);

                foreach (Match m in mc)
                {
                    nvc.Add(m.Result("$2"), m.Result("$3"));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
