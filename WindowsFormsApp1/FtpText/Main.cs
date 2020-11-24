using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FtpText
{
    public class Main
    {
        string FTPAddress = "ftp://219.232.86.2:2123"; //ftp服务器地址
        string FTPUsername = "tcc_jcsj2020";   //用户名
        string FTPPwd = "tcc_jcsj20203306761";

        //上传
        public void UpFile()
        {
            string LocalPath = "F:\\text.txt"; //待上传文件
            FileInfo f = new FileInfo(LocalPath);
            string FileName = f.Name;
            //Path = Path.Replace("\\", "/");
            string ftpRemotePath = "/text/";
            string FTPPath = FTPAddress + ftpRemotePath+ FileName; //上传到ftp路径,如ftp://***.***.***.**:21/home/test/test.txt
            //实现文件传输协议 (FTP) 客户端
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd); //设置通信凭据
            reqFtp.KeepAlive = false; //请求完成后关闭ftp连接
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            //读本地文件数据并上传
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.Write("上传失败");
            }
        }

        //下载
        public void DownFile()
        {
            string FtpFilePath = "/home/test.txt";   //远程路径
            string LocalPath = "F:\\ftp\\test.txt"; //下载到的本地路径
            if (File.Exists(LocalPath))
            {
                File.Delete(LocalPath);
            }
            string FTPPath = FTPAddress + FtpFilePath;
            //建立ftp连接
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int buffersize = 2048;
            int readCount;
            byte[] buffer = new byte[buffersize];
            readCount = ftpStream.Read(buffer, 0, buffersize);
            //创建并写入文件
            FileStream OutputStream = new FileStream(LocalPath, FileMode.Create);
            while (readCount > 0)
            {
                OutputStream.Write(buffer, 0, buffersize);
                readCount = ftpStream.Read(buffer, 0, buffersize);
            }
            ftpStream.Close();
            OutputStream.Close();
            response.Close();
            if (File.Exists(LocalPath))
                Console.Write("下载完毕");
        }
    }
}
