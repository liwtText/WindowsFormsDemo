using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace MEFDemo
{
    [Export(typeof(Interface1))]
    class Class1 : Interface1
    {
        public string GetBookName()
        {
            return "音乐数据";
        }
    }
}
