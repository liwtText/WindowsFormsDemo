using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary2;

namespace WindowsFormsApp4
{
    class Class1
    {
        [DllImport("ClassLibrary2.dll")]
        public static extern void ShowMessage(string text);//动态链接库中方法
    }
}
