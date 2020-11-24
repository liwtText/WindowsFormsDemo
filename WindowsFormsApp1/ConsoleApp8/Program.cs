using System;
using System.Threading;
using System.Net;
using System.Reflection;

namespace ConsoleApp8
{
    class Program
    {
        public  static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();

        static void Main(string[] args)
        {
           // string ip1 = GetLocalIp();
           ////string ip = System.Net.Dns.Resolve(IP地址).HostName;
           // Console.WriteLine("Hello World!" + ip1 + "ip2");

            var people = new People()
            {
                Name = "qweas",
                Description = "description"
            };
            try
            {
                new ValidationModel().Validate(people);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        public static string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
        //static void Main(string[] args)
        //{
        //    sw.Start();
        //    s.Start();
        //    //获取正在运行的线程
        //    Thread thread = new Thread(new ParameterizedThreadStart(Thread1));
        //                //给方法传值
        //    thread.Start("这是一个有参数的委托");
        //    sw.Stop();
        //    TimeSpan ts2 = sw.Elapsed;
        //    Console.WriteLine("Hello World!" + ts2.TotalMilliseconds);
        //}
        //public static void Thread1(object obj)
        //{
        //    s.Stop();
        //    TimeSpan ts2 = s.Elapsed;
        //    Console.WriteLine(obj +":"+ ts2.TotalMilliseconds);
        //}
    }
    public class People
    {
        [StringLength(8)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Description { get; set; }

        public string text { get; set; }
    }
    public class ValidationModel
    {

        public void Validate(object obj)
        {
            var t = obj.GetType();

            //由于我们只在Property设置了Attibute,所以先获取Property
            var properties = t.GetProperties();
            foreach (var property in properties)
            {

                //这里只做一个stringlength的验证，这里如果要做很多验证，需要好好设计一下,千万不要用if elseif去链接
                //会非常难于维护，类似这样的开源项目很多，有兴趣可以去看源码。
                if (!property.IsDefined(typeof(StringLengthAttribute), false)) continue;

                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    //这里的MaximumLength 最好用常量去做
                    var maxinumLength = (int)attribute.GetType().
                      GetProperty("MaximumLength").
                      GetValue(attribute);

                    //获取属性的值
                    var propertyValue = property.GetValue(obj) as string;
                    if (propertyValue == null)
                        throw new Exception("exception info");//这里可以自定义，也可以用具体系统异常类

                    if (propertyValue.Length > maxinumLength)
                        throw new Exception(string.Format("属性{0}的值{1}的长度超过了{2}", property.Name, propertyValue, maxinumLength));
                }
            }

        }
    }
}
