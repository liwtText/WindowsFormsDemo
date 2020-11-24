using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnitInter;

namespace InterfaceServer
{
    public class TextName : IWebService
    {
        public TextName(TransProp transProp, IPackage package)
            : base(transProp, package)
        {

        }
        public override string DoWork(string request)
        {
            throw new NotImplementedException();
        }
    }
}
