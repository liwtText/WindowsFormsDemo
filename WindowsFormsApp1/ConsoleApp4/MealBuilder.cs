using ConsoleApp4.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public class MealBuilder
    {
        public Meal prepareVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new VegBurger());
            return meal;
        }

        public Meal prepareNonVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new Pepsi());
            return meal;
        }
    }
}
