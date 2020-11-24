using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsService3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Class1 class1 = new Class1();
            class1.url = this.textBox1.Text;
            class1.pageStart = (int)this.numericUpDown1.Value;
            class1.pageEnd = (int)this.numericUpDown2.Value;
            class1.suffix = this.textBox5.Text;
            class1.pathName = this.textBox2.Text;
            class1.htmlImgUrl = this.textBox3.Text;
            class1.imgUrl = this.textBox4.Text;
            Main main = new Main();
            string resMsg = main.Start(class1);
            MessageBox.Show(resMsg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1 class1 = new Class1();
            class1.url = this.textBox1.Text;
            class1.pageStart = (int)this.numericUpDown1.Value;
            class1.pageEnd = (int)this.numericUpDown2.Value;
            class1.suffix = this.textBox5.Text;
            class1.pathName = this.textBox2.Text;
            class1.htmlImgUrl = this.textBox3.Text;
            class1.imgUrl = this.textBox4.Text;
            Main main = new Main();
            for (int i = class1.pageStart; i <= class1.pageEnd; i++)
            {
                string url1 = Path.Combine(class1.url, i + class1.suffix);
                HtmlAgilityPack.HtmlDocument retStr = HttpPostInfo.Get(url1);
                main.Start1(retStr, class1, i);
            }
            MessageBox.Show("执行完毕");

        }
    }
}
