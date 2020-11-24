using ConsoleApp5.Abstract;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    public class ShapeCache
    {
        //ConcurrentDictionary
        private static Dictionary<string, Shape> shapeMap = new Dictionary<string, Shape>();

        public static Shape getShape(String shapeId)
        {
            
            Shape cachedShape = shapeMap[shapeId] as Shape;
            return cachedShape;
        }

        // 对每种形状都运行数据库查询，并创建该形状
        // shapeMap.put(shapeKey, shape);
        // 例如，我们要添加三种形状
        public static void loadCache()
        {

            Square square = new Square();
            square.setId("2");
            shapeMap.Add(square.getId(), square);

            Rectangle rectangle = new Rectangle();
            rectangle.setId("3");
            shapeMap.Add(rectangle.getId(), rectangle);
        }
    }
}
