using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5.Abstract
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            type = "Rectangle";
        }

        public void draw()
        {
            Console.WriteLine("Inside Rectangle::draw() method.");
        }
    }
}
