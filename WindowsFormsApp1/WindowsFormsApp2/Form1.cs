using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBoxTimeOut timeOut = new MessageBoxTimeOut();
            timeOut.Show("定时关闭弹框","弹框");
            DateTime time = DateTime.Now;
            double jmhite = 1.5;
            string NewTime = time.AddHours(jmhite).ToString();
            char a = NewTime[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string AesIV = "x7KV3etJ3z8D6yjV";
            string AesKey = "6kPy0elrvBmFY1rF";
            string text ="oTspl62gKY4yHPlizcbDijQdmAutWJ7+puPyeqRfxqZKnnJCHQwFo17xf03WUlnm/uIhufNIWl+zcJn5i23MOysVmVGO91yDdO2fzvJ04N1vxQe8b9o3alUenBTKbh7vd+2AFesn48dMYRGOTCEfwEIwv8e9y1dTwTptVVSsPDjx7DFLnZvMeFpcsz8JRJuHSkVQlnKXKL+mcROCSigjEOWo8JXk0oHJ3zkAGq2dxjVWpHOtMEJLyU3Oj8+pRQ25IrU1UAELT6zatGFnVviWPdbYPpO7EhQfGaAqy8gLFgVBXgOUtpUYHYyV+NmaT1bXXzAmb4eYio9KohSTZGFaHWGBeXWxnXq4+jS6Cg90Pri3LMhOj+eEQCsJOaeOVNkIJPYmbGyyG2RpzkqP1mQeCxSnpeeugR86akObR2ijRgmysPpB8/W5ygNSuUirsmrHrbJ8bM58YfODjH/VAcLSb/JCDzQm9qE2lll51hRXQjvnfp05Oh0ky0cDTEw5FbCz1SVmycQj44oqoeBE7i6THw==";
            string s = AESHelper.AesDecrypt(text,AesKey,AesIV);
            Console.WriteLine(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s1 = "{\"openId\":\"oGZUI0egBJY1zhBYw2KhdUfwVJJE\",\"nickName\":\"Band\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Guangzhou\",\"province\":\"Guangdong\",\"country\":\"CN\",\"avatarUrl\":\"http://wx.qlogo.cn/mmopen/vi_32/aSKcBBPpibyKNicHNTMM0qJVh8Kjgiak2AHWr8MHM4WgMEm7GFhsf8OYrySdbvAMvTsw3mo8ibKicsnfN5pRjl1p8HQ/0\",\"unionId\":\"ocMvos6NjeKLIBqg5Mr9QjxrP1FA\",\"watermark\":{ \"timestamp\":1477314187,\"appid\":\"wx4f4bc4dec97d474b\"}";

            string AesIV = "x7KV3etJ3z8D6yjV";
            string AesKey = "6kPy0elrvBmFY1rF";
            string s = AESHelper.AesEncrypt(s1.Trim(),  AesKey, AesIV);
            Console.WriteLine(s);
        }
    
    }
}
