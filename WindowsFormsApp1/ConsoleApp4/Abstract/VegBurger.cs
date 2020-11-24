using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Abstract
{
    public class VegBurger:Burger
    {
        public float price()
        {
            return 25.0f;
        }

        public String name()
        {
            return "Veg Burger";
        }
    }
}
