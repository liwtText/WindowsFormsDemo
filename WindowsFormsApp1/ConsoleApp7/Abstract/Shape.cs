using ConsoleApp7.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp7.Abstract
{
    public abstract class Shape
    {
        protected DrawAPI drawAPI;
        protected int x, y, radius;

        protected Shape(int x, int y, int radius, DrawAPI drawAPI)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.drawAPI = drawAPI;
        }
        public abstract void Draw();
    }
}
