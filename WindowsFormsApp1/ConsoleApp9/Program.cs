﻿using System;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ServiceDefinationXml DefinationXml = PublicFunc.XmlSerializeToObject<ServiceDefinationXml>("ServiceDefination.xml");

        }
    }
}