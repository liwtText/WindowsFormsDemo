using ConsoleApp7.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp7.Implements
{
    public class GreenCircle : DrawAPI
    {
        public void DrawCircle(int radius, int x, int y)
        {
            Console.WriteLine("Drawing Circle[ color: green, radius: "
          + radius + ", x: " + x + ", " + y + "]");
        }
    }
}
