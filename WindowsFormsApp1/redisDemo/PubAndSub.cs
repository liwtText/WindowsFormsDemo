using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace redisDemo
{
    public class PubAndSub
    {
        public static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");//如果没有密码就直接写ip地址和端口
        public static ISubscriber sub = redis.GetSubscriber();
        public static void pushRedis()
        {
            try
            {
                sub.Subscribe("runoobChat", (channel, message) =>
                {
                    Console.WriteLine("订阅收到消息：" + message);
                });
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Test()
        {
            Console.WriteLine("请输入发布订阅类型?");
            var type = Console.ReadLine();
            if (type == "publish")
            {
                while (true)
                {
                    Console.WriteLine("请输入要发布向哪个通道？");
                    var channel = Console.ReadLine();
                    Console.WriteLine("请输入要发布的消息内容.");
                    var message = Console.ReadLine();
                    sub.Publish(channel, message);
                }
            }
            else
            {
                Console.WriteLine("请输入您要订阅哪个通道的信息？");
                var channelKey = Console.ReadLine();
                sub.Subscribe(channelKey, (channel, message) =>
                {
                    Console.WriteLine("接受到发布的内容为：" + message);
                });
                Console.WriteLine("您订阅的通道为：<< " + channelKey + " >> ! 一切就绪，等待发布消息！勿动，一动就没啦！！");
                Console.ReadKey();
            }
        }
    }
}
