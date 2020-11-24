using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //向指定地址发送请求
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("https://home.firefoxchina.cn");
            //HttpWReq.Proxy = proxyObject;
            HttpWReq.Timeout = 50000;
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(sr);
            HtmlNodeCollection ulNodes = doc.DocumentNode.SelectSingleNode("//div[@id='pane-news']").SelectNodes("ul");
            if (ulNodes != null && ulNodes.Count > 0)
            {
                for (int i = 0; i < ulNodes.Count; i++)
                {
                    HtmlNodeCollection liNodes = ulNodes[i].SelectNodes("li");
                    for (int j = 0; j < liNodes.Count; j++)
                    {
                        string title = liNodes[j].SelectSingleNode("a").InnerHtml.Trim();
                        string href = liNodes[j].SelectSingleNode("a").GetAttributeValue("href", "").Trim();
                        Console.WriteLine("新闻标题：" + title + ",链接：" + href);
                    }
                }
            }
            Console.ReadLine();
            sr.Close();
            HttpWResp.Close();
            HttpWReq.Abort();
        }
    }
}
