using ConsoleApp7.Abstract;
using ConsoleApp7.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp7.extends
{
    public class Circle : Shape
    {
        //private int x, y, radius;
        public Circle(int x, int y, int radius, DrawAPI drawAP):base(x,y,radius, drawAP)
        {

        }

        public override void Draw()
        {
            drawAPI.DrawCircle(radius, x, y);
        }

    }
}
