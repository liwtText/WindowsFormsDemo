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
    /// <summary>
    /// C# WinForm 跨线程访问控件
    /// </summary
    public partial class Form1 : Form
    {
        /// //三
        private SynchronizationContext _synccontext;
        //四
        BackgroundWorker bw = new BackgroundWorker();
        public Form1()
        {
            InitializeComponent();
            //一
            Control.CheckForIllegalCrossThreadCalls = false;
            new Thread(new ThreadStart(UpDate)).Start();
            //二
            new Thread(() => Update()).Start();
            //三
            _synccontext = SynchronizationContext.Current;
            new Thread(() => Update()).Start();
            //四
            bw.DoWork += new DoWorkEventHandler(UpDate);
            bw.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(UpDate1);
            bw.RunWorkerAsync("123");

        }

        private void UpDate()
        {
            //一
            this.textBox1.Text = "123";
            //二
            Action act = delegate () { this.textBox1.Text = "123"; };
            this.Invoke(act);
            //三
            _synccontext.Post(_ => this.textBox1.Text = "123", null);
        }

        //四
         void UpDate(object sender,DoWorkEventArgs e)
        {
            e.Result = e.Argument.ToString();
        }
        //四
         void UpDate1(object sender ,RunWorkerCompletedEventArgs e)
        {
            this.textBox1.Text = e.Result.ToString();
        }
    }
}
