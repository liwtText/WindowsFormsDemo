using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;//引入动态链接库
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //引入动态链接库
        [DllImport("ClassLibrary2.dll")]
        public static extern void ShowMessage(string text);//动态链接库中方法

        private void button1_Click(object sender, EventArgs e)
        {
            ClassLibrary2.Class1 I = new ClassLibrary2.Class1();
            I.ShowMessage("弹框");
        }
    }
}
