using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebAPI.Common;

namespace Log4Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoggerHelper.WriteLog(typeof(Form1), "ErrorMessage");
        }
    }
}
