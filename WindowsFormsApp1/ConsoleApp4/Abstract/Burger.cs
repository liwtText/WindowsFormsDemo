using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public abstract class Burger : Intem
    {
        public string name()
        {
            throw new NotImplementedException();
        }

        public Packing packing()
        {
            return new Wrapper();
        }

        public  float price()
        {
            throw new NotImplementedException();
        }
    }
}
