using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp12
{

    public class ErrorLogger : AbstractLogger
    {


        public ErrorLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("Error Console::Logger: " + message);
        }
    }

}
