using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            //通过委托来实现异步编程的（在委托类型中定义了BeginInvoke()和EndInvoke()两个方法）
            string i = "参数";
            Console.WriteLine("调用异步方法前");
            PostAsync(i);
            Console.WriteLine("调用异步方法后");


            Console.WriteLine("主线程，线程ID：" + Thread.CurrentThread.ManagedThreadId);
            //Task方式一
            Task task1 = new Task(() => TaskFunc1());
            task1.Start();
            //Task方式二
            var result = Task.Run<string>(() => { return TaskFunc2(); });
            Console.WriteLine(result.Result);


            var result1 = AsyncFunc1();
            Console.WriteLine(result1.Result);

            Console.ReadKey();
        }

        delegate void AsyncFoo(string i);
        private static void PostAsync(object i)
        {
            try
            {
                AsyncFoo caller = Myfunc;
                caller.BeginInvoke(i.ToString(), FooCallBack, caller);
            }
            catch (Exception es)
            {

                throw;
            }
        }

        //回调方法
        private static void FooCallBack(IAsyncResult ar)
        {
            var caller = (AsyncFoo)ar.AsyncState;
            caller.EndInvoke(ar);
        }

        /// <summary>
        /// 执行业务逻辑的方法
        /// </summary>
        /// <param name="i">调用异步时传过来的参数</param>
        private static void Myfunc(string i)
        {
            Console.WriteLine("通过委托来实现异步编程的");
        }

        private static void TaskFunc1()
        {
            Console.WriteLine("Task方式一开启的线程ID：" + Thread.CurrentThread.ManagedThreadId);
        }

        private static string TaskFunc2()
        {
            return "Task方式二开启的线程ID：" + Thread.CurrentThread.ManagedThreadId;
        }

        private static async Task<string> AsyncFunc1()
        {
            return await Task.Run(() =>
            {
                Console.WriteLine("await/async线程ID: {0}", Thread.CurrentThread.ManagedThreadId);
                return "这是返回值";
            });

        }

    }
}
