using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public  class Meal
    {
        private List<Intem> items = new List<Intem>();
        public void addItem(Intem item)
        {
            items.Add(item);
        }

        public float getCost()
        {
            float cost = 0.0f;
            foreach (Intem item in items)
            {
                cost += item.price();
            }
            return cost;
        }

        public void showItems()
        {
            foreach (Intem item in items)
            {
                //System.out.print("Item : " + item.name());
                //System.out.print(", Packing : " + item.packing().pack());
                //System.out.println(", Price : " + item.price());
            }
          
        }
    }
}
