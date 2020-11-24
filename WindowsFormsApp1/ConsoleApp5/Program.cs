using System;
using ConsoleApp5.Abstract;
namespace ConsoleApp5
{
    class Program
    {
        public static void Main(String[] args)
        {
            ShapeCache.loadCache();

            Shape clonedShape2 = (Shape)ShapeCache.getShape("2");
            Console.WriteLine("Shape : " + clonedShape2.getType());

            Shape clonedShape3 = (Shape)ShapeCache.getShape("3");
            Console.WriteLine("Shape : " + clonedShape3.getType());
        }
    }
}
