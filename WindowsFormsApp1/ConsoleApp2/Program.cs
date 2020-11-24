using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    //class Program
    //{
    //    private static object o = new object();
    //    private static List<Product> _Products { get; set; }
    //    /*  coder:天才卧龙  
    //     *  代码中 创建三个并发线程 来操作_Products 集合
    //     *  System.Collections.Generic.List 这个列表在多个线程访问下，不能保证是安全的线程，所以不能接受并发的请求，我们必须对ADD方法的执行进行串行化
    //     */
    //    static void Main(string[] args)
    //    {
    //        _Products = new List<Product>();
    //        /*创建任务 t1  t1 执行 数据集合添加操作*/
    //        Task t1 = Task.Factory.StartNew(() =>
    //        {
    //            AddProducts();
    //        });
    //        /*创建任务 t2  t2 执行 数据集合添加操作*/
    //        Task t2 = Task.Factory.StartNew(() =>
    //        {
    //            AddProducts();
    //        });
    //        /*创建任务 t3  t3 执行 数据集合添加操作*/
    //        Task t3 = Task.Factory.StartNew(() =>
    //        {
    //            AddProducts();
    //        });
    //        Task.WaitAll(t1, t2, t3);
    //        Console.WriteLine(_Products.Count);
    //        Console.ReadLine();
    //    }

    //    /*执行集合数据添加操作*/
    //    static void AddProducts()
    //    {
    //        Parallel.For(0, 1000, (i) =>
    //        {
    //            Product product = new Product();
    //            product.Name = "name" + i;
    //            product.Category = "Category" + i;
    //            product.SellPrice = i;
    //            _Products.Add(product);
    //        });

    //        //使用LOCK(互斥锁)是可以做到防止并发
    //        Parallel.For(0, 1000, (i) =>
    //        {
    //            lock (o)
    //            {

    //                Product product = new Product();
    //                product.Name = "name" + i;
    //                product.Category = "Category" + i;
    //                product.SellPrice = i;
    //                _Products.Add(product);
    //            }
    //        });
    //    }
    //}


    class Program
    {
        private static object o = new object();
        /*定义 Queue*/
        private static Queue<Product> _Products { get; set; }
        private static ConcurrentQueue<Product> _ConcurrenProducts { get; set; }// 提供线程安全的先进先出集合

        /*  coder:天才卧龙  
         *  代码中 创建三个并发线程 来操作_Products 和 _ConcurrenProducts 集合，每次添加 10000 条数据 查看 一般队列Queue 和 多线程安全下的队列ConcurrentQueue 执行情况
         */
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            _Products = new Queue<Product>();
            Stopwatch swTask = new Stopwatch();//用于统计时间消耗的
            swTask.Start();

            /*创建任务 t1  t1 执行 数据集合添加操作*/
            Task t1 = Task.Factory.StartNew(() =>
            {
                AddProducts();
            });
            /*创建任务 t2  t2 执行 数据集合添加操作*/
            Task t2 = Task.Factory.StartNew(() =>
            {
                AddProducts();
            });
            /*创建任务 t3  t3 执行 数据集合添加操作*/
            Task t3 = Task.Factory.StartNew(() =>
            {
                AddProducts();
            });

            Task.WaitAll(t1, t2, t3);
            swTask.Stop();
            Console.WriteLine("List<Product> 当前数据量为：" + _Products.Count);
            Console.WriteLine("List<Product> 执行时间为：" + swTask.ElapsedMilliseconds);

            Thread.Sleep(1000);
            _ConcurrenProducts = new ConcurrentQueue<Product>();
            Stopwatch swTask1 = new Stopwatch();
            swTask1.Start();

            /*创建任务 tk1  tk1 执行 数据集合添加操作*/
            Task tk1 = Task.Factory.StartNew(() =>
            {
                AddConcurrenProducts();
            });
            /*创建任务 tk2  tk2 执行 数据集合添加操作*/
            Task tk2 = Task.Factory.StartNew(() =>
            {
                AddConcurrenProducts();
            });
            /*创建任务 tk3  tk3 执行 数据集合添加操作*/
            Task tk3 = Task.Factory.StartNew(() =>
            {
                AddConcurrenProducts();
            });

            Task.WaitAll(tk1, tk2, tk3);
            swTask1.Stop();
            Console.WriteLine("ConcurrentQueue<Product> 当前数据量为：" + _ConcurrenProducts.Count);
            Console.WriteLine("ConcurrentQueue<Product> 执行时间为：" + swTask1.ElapsedMilliseconds);
            Console.ReadLine();
        }

        /*执行集合数据添加操作*/

        /*执行集合数据添加操作*/
        static void AddProducts()
        {
            Parallel.For(0, 30000, (i) =>
            {
                Product product = new Product();
                product.Name = "name" + i;
                product.Category = "Category" + i;
                product.SellPrice = i;
                lock (o)
                {
                    _Products.Enqueue(product);
                }
            });

        }
        /*执行集合数据添加操作*/
        static void AddConcurrenProducts()
        {
            Parallel.For(0, 30000, (i) =>
            {
                Product product = new Product();
                product.Name = "name" + i;
                product.Category = "Category" + i;
                product.SellPrice = i;
                _ConcurrenProducts.Enqueue(product);
            });

        }
    }

    class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SellPrice { get; set; }
    }

}
