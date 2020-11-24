#define canUse
using System;
using System.Diagnostics;
using System.Reflection;

namespace AttributeTest
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    MethodOne();
        //    MethodTwo();

        //    Console.WriteLine("Hello World!");
        //    OldMethod();
        //}

        //static void Main(string[] args)
        //{
        //    System.Reflection.MemberInfo info = typeof(MyClass);
        //    object[] attributes = info.GetCustomAttributes(true);
        //    for (int i = 0; i < attributes.Length; i++)
        //    {
        //        System.Console.WriteLine(attributes[i]);
        //    }
        //    Console.ReadKey();

        //}

        //static void Main(string[] args)
        //{
        //    Rectangle r = new Rectangle(4.5, 7.5);
        //    r.Display();
        //    Type type = typeof(Rectangle);
        //    // 遍历 Rectangle 类的特性
        //    foreach (Object attributes in type.GetCustomAttributes(false))
        //    {
        //        DeBugInfo dbi = (DeBugInfo)attributes;
        //        if (null != dbi)
        //        {
        //            Console.WriteLine("Bug no: {0}", dbi.BugNo);
        //            Console.WriteLine("Developer: {0}", dbi.Developer);
        //            Console.WriteLine("Last Reviewed: {0}",
        //                                     dbi.LastReview);
        //            Console.WriteLine("Remarks: {0}", dbi.Message);
        //        }
        //    }

        //    // 遍历方法特性
        //    foreach (MethodInfo m in type.GetMethods())
        //    {
        //        foreach (Attribute a in m.GetCustomAttributes(true))
        //        {
        //            DeBugInfo dbi = (DeBugInfo)a;
        //            if (null != dbi)
        //            {
        //                Console.WriteLine("Bug no: {0}, for Method: {1}",
        //                                              dbi.BugNo, m.Name);
        //                Console.WriteLine("Developer: {0}", dbi.Developer);
        //                Console.WriteLine("Last Reviewed: {0}",
        //                                              dbi.LastReview);
        //                Console.WriteLine("Remarks: {0}", dbi.Message);
        //            }
        //        }
        //    }
        //    Console.ReadLine();
        //}

        static void Main(string[] args)
        {
            IndexedNames index = new IndexedNames(5);
            index[0] = "Zara";
            index[1] = "Riz";
            index[2] = "Nuha";
            index[3] = "Asif";
            index[4] = "Davinder";
            for (int i = 0; i < index.size; i++)
            {
                Console.WriteLine(index[i]);
            }
            // 使用带有 string 参数的第二个索引器
            //Console.WriteLine(names["Nuha"]);
            Console.ReadKey();
        }


        class IndexedNames
        {
            private string[] nameList;
            public int size = 10;
            public IndexedNames(int size)
            {
                this.size = size;
                nameList = new string[size];
                for (int i = 0; i < size; i++)
                {
                    nameList[i] = "N. A.";
                }
            }
            public string this[int index]
            {
                get
                {
                    string tmp;

                    if (index >= 0 && index <= size - 1)
                    {
                        tmp = nameList[index];
                    }
                    else
                    {
                        tmp = "";
                    }

                    return (tmp);
                }
                set
                {
                    if (index >= 0 && index <= size - 1)
                    {
                        nameList[index] = value;
                    }
                }
            }
            public int this[string name]
            {
                get
                {
                    int index = 0;
                    while (index < size)
                    {
                        if (nameList[index] == name)
                        {
                            return index;
                        }
                        index++;
                    }
                    return index;
                }

            }
        }
        [Conditional("canUse")]
        static void MethodOne()
        {
            Console.WriteLine("11111");
        }
        static void MethodTwo()
        {
            Console.WriteLine("22222");
        }

        //Obsolete特性用来表示一个方法被弃用了，并显示有用的警告信息
        [Obsolete("该方法已被弃用，请使用NewMethod代替", true)]
        static void OldMethod()
        {
            Console.WriteLine("Old Method.");
        }

        static void NewMethod()
        {
            Console.WriteLine("New Method.");
        }


    }

}
