using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp12
{

    public class ConsoleLogger : AbstractLogger
    {


        public ConsoleLogger(int level)
        {
            this.level = level;
        }

        protected override void write(string message)
        {
            Console.WriteLine("Standard Console::Logger: " + message);
        }
    }
}
