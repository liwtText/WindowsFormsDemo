using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Abstract
{
    public class Pepsi : ColdDrink
    {
        public float price()
        {
            return 35.0f;
        }

        public String name()
        {
            return "Pepsi";
        }
    }
}
