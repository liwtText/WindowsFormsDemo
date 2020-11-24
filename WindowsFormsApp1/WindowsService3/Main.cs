using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsService3
{
    public class Main
    {
        static Queue<string> q = new Queue<string>();
        public string Start(Class1 class1)
        {

            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "HtmlText.text");
            //if (!File.Exists(path))
            //{
            //    File.Create(path);
            //}

            //FileInfo fileInfo = new FileInfo(path);
            //if (!fileInfo.Exists)
            //{
            //    fileInfo.CreateText();
            //}
            //string path1 = fileInfo.DirectoryName;

            //DateTime time = Directory.GetCreationTime(Environment.CurrentDirectory);

            //FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate,FileAccess.Write,FileShare.None);

            //FileStream file = File.Open(path,FileMode.OpenOrCreate,FileAccess.ReadWrite);
            //StreamWriter sw = new StreamWriter(file,Encoding.Default);
            //sw.Write("");
            //sw.Close();
            //file.Close(); url ="http://www.kuman.com/mh-1002933/200/"   
            //https://www.91hanman.com/page/get/130/1/false
            //https://www.91hanman.com/page/get/22/5/false
            //https://m.bnmanhua.com/comic/10237/1212736.html?p=2

            for (int i = class1.pageStart; i <= class1.pageEnd; i++)
            {
                try
                {
                    string url1 = Path.Combine(class1.url, i + class1.suffix);
                    HttpWebRequest HttpWReq = null;
                    HttpWebResponse HttpWResp = null;
                    HttpWReq = (HttpWebRequest)WebRequest.Create(url1);
                    HttpWReq.Method = "GET";
                    HttpWReq.Timeout = 100000;
                    HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                    StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.Load(sr);

                    HtmlNode responseNew = doc.DocumentNode.SelectSingleNode("/html/body");
                    //HtmlNodeCollection ulNodes1 = responseNew.SelectNodes("div[1]/div[1]/section[1]/ul[1]/li");
                    HtmlNodeCollection ulNodes = responseNew.SelectNodes(class1.htmlImgUrl);

                    //List<string> url = new List<string>();
                    foreach (HtmlNode item in ulNodes)
                    {
                        string infourl = string.Empty;
                        if (string.IsNullOrEmpty(class1.imgUrl))
                        {
                            var xpath = item.XPath;
                            infourl = item.SelectSingleNode(xpath + "/img").Attributes["data-src"].Value; //url
                            //url.Add(infourl);
                        }
                        else
                        {
                            HtmlNodeCollection titleName = item.SelectNodes(class1.imgUrl);
                            foreach (HtmlNode item1 in titleName)
                            {
                                var xpath = item1.XPath;
                                infourl = item.SelectSingleNode(xpath + "/img").Attributes["data-src"].Value; //url
                                //url.Add(infourl);
                            }
                        }

                        string[] name = infourl.Split('/');
                        HttpWReq = (HttpWebRequest)WebRequest.Create(infourl);
                        HttpWReq.Method = "GET";
                        HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                        Stream stream = HttpWResp.GetResponseStream();
                        string path = "G:\\Img\\" + class1.pathName + "\\" + i;
                        if (!Directory.Exists(path))  //判断是否存在某个文件夹
                        {
                            Directory.CreateDirectory(path);    //创建文件夹
                        }
                        string imgJpg = Path.Combine(path, name[3] + ".jpg");

                        System.Drawing.Image img;
                        img = System.Drawing.Image.FromStream(stream);
                        img.Save(imgJpg, ImageFormat.Jpeg);
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, ImageFormat.Jpeg);
                        img.Dispose();
                    }
                    HttpWResp.Close();
                    HttpWReq.Abort();
                    //关闭连接和流
                    if (HttpWResp != null)
                    {
                        HttpWResp.Close();
                    }
                    if (HttpWReq != null)
                    {
                        HttpWReq.Abort();
                    }
                    //foreach (var item in url)
                    //{
                    //    //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "HtmlText.text");
                    //    //FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //    //StreamWriter sw = new StreamWriter(file, Encoding.Default);
                    //    //sw.Write(img);
                    //}
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                //return "下载成功";
            }
            return "下载成功";
        }

        public void Start1(HtmlAgilityPack.HtmlDocument doc, Class1 class1,int i)
        {
            try
            {
                HtmlNode responseNew = doc.DocumentNode.SelectSingleNode("/html/body");
                HtmlNodeCollection ulNodes1 = responseNew.SelectNodes("div[1]/div[1]/div[1]/div[2]/h1");
                string titleName1 = string.Empty;
                foreach (var item in ulNodes1)
                {
                    titleName1 = item.InnerText;
                }
                if (string.IsNullOrEmpty(titleName1))
                {
                    titleName1 = "第" + i + "话";
                }

                HtmlNodeCollection ulNodes = responseNew.SelectNodes(class1.htmlImgUrl);
                int j = 0;
                foreach (HtmlNode item in ulNodes)
                {
                    j++;
                    string infourl = string.Empty;
                    if (string.IsNullOrEmpty(class1.imgUrl))
                    {
                        var xpath = item.XPath;
                        infourl = item.SelectSingleNode(xpath + "/img").Attributes["data-src"].Value; //url
                    }
                    else
                    {
                        HtmlNodeCollection titleName = item.SelectNodes(class1.imgUrl);
                        foreach (HtmlNode item1 in titleName)
                        {
                            var xpath = item1.XPath;
                            infourl = item.SelectSingleNode(xpath + "/img").Attributes["src"].Value; //url
                        }
                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(infourl);
                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    string path = "G:\\Img\\" + class1.pathName;
                    if (!Directory.Exists(path))  //判断是否存在某个文件夹
                    {
                        Directory.CreateDirectory(path);    //创建文件夹
                    }

                    string imgJpg = Path.Combine(path, titleName1 + "第"+ j + "页.jpg");
                    if (File.Exists(imgJpg))
                    {
                        continue;
                    }
                    System.Drawing.Image img;
                    img = System.Drawing.Image.FromStream(stream);
                    img.Save(imgJpg, ImageFormat.Jpeg);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Jpeg);
                    img.Dispose();
                }
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.ToString());
                log.WriteLog("第" + i + "话报错"+es.ToString());
            }
        }
    }
}
