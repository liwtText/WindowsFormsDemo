using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2
{
    public class Class1
    {
        public void ShowMessage(string text)
        {
            Console.WriteLine("你调用了动态链接库！");
            MessageBox.Show(text, "消息提示");
        }
    }
}
