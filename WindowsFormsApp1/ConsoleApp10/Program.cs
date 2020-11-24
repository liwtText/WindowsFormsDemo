using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            var toWrap = new EchoImpl();
            var decorator = DispatchProxy.Create<IEcho, GenericDecorator>();
            ((GenericDecorator)decorator).Wrapped = toWrap;
            ((GenericDecorator)decorator).Start = (tm, a) => Console.WriteLine($"{tm.Name}({string.Join(',', a)})方法开始调用");
            ((GenericDecorator)decorator).End = (tm, a, r) => Console.WriteLine($"{tm.Name}({string.Join(',', a)})方法结束调用，返回结果{r}");
            decorator.Echo("Echo");
            decorator.Method("Method");
        }
    }
    class GenericDecorator : DispatchProxy
    {
        public object Wrapped { get; set; }
        public Action<MethodInfo, object[]> Start { get; set; }
        public Action<MethodInfo, object[], object> End { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Start(targetMethod, args);
            object result = targetMethod.Invoke(Wrapped, args);
            End(targetMethod, args, result);
            return result;
        }
    }

    interface IEcho
    {
        void Echo(string message);
        string Method(string info);
    }

    class EchoImpl : IEcho
    {
        public void Echo(string message) => Console.WriteLine($"Echo参数：{message}");

        public string Method(string info)
        {
            Console.WriteLine($"Method参数：{info}");
            return info;
        }
    }
}
