using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5.Abstract
{
    public class Square: Shape
    {
        public Square()
        {
            type = "Square";
        }

        public void draw()
        {
            Console.WriteLine("Inside Square::draw() method.");
        }
    }
}
