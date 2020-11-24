using ConsoleApp7.Abstract;
using ConsoleApp7.extends;
using ConsoleApp7.Implements;
using System;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape redCircle = new Circle(100, 100, 10, new RedCircle());
            Shape greenCircle = new Circle(111, 111, 11, new GreenCircle());

            redCircle.Draw();
            greenCircle.Draw();
        }
    }
}
