using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // 异步线程访问控件
    public partial class Form2 : Form
    {
        delegate void AsyncFoo(string i);
        event AsyncFoo testevent;
        public Form2()
        {
            InitializeComponent();
            //testevent += new AsyncFoo(Myfunc);
        }
        private  void PostAsync()
        { 
            Task.Factory.StartNew(() =>
            {
                //Thread.Sleep(5000);
                string i = "参数当前线程" + Thread.CurrentThread.ManagedThreadId;
                //deleText dele = new deleText(Myfunc);
                //this.Invoke(dele, i);

                Myfunc(i);
            });
        }
        //private static void FooCallBack(IAsyncResult ar)
        //{
        //    var caller = (AsyncFoo)ar.AsyncState;
        //    caller.EndInvoke(ar);
        //}
        delegate void deleText(string i);
        public  void Myfunc(string i)
        {
            if (this.InvokeRequired)
            {
                deleText dele = new deleText(Myfunc);
                this.Invoke(dele, i);
            }
            else this.textBox1.Text = i+"线程" + Thread.CurrentThread.ManagedThreadId;
        }
        public void Test()
        {
            this.Invoke((Action)(() =>///222
            {
                this.textBox2.Text = "线程aaa" + Thread.CurrentThread.ManagedThreadId;
            }));
        }
        private delegate void dlClose();
        //public void CloseForm(IAsyncResult ar)
        //{
        //    this.Invoke(new dlClose(() => { this.Close(); }));
        //}
        private void Button1_Click(object sender, EventArgs e)
        {
            //Form2 fmWait = new Form2(); //异步线程加载遮罩层
            //new Action(Test).BeginInvoke(new AsyncCallback(fmWait.CloseForm), null);
            //fmWait.ShowDialog();
            this.textBox2.Text = "主窗体按钮";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string i = "参数当前线程" + Thread.CurrentThread.ManagedThreadId;
            PostAsync();
            new Thread(() => Test()).Start();
        }
    }
}
