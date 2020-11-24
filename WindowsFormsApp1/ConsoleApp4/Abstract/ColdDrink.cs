using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public abstract class ColdDrink : Intem
    {
        public string name()
        {
            throw new NotImplementedException();
        }

        public Packing packing()
        {
            return new Bottle();
        }

        public float price()
        {
            throw new NotImplementedException();
        }
    }
}
