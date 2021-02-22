## 0.介绍
> RabbitMQ是实现了高级消息队列协议（AMQP）的开源消息代理软件（亦称面向消息的中间件）。RabbitMQ服务器是用Erlang语言编写的，而集群和故障转移是构建在开放电信平台框架上的。所有主要的编程语言均有与代理接口通讯的客户端库。

本来想整理项目对RabbitMQ的使用，但是发现对RabbitMQ使用太粗浅，发现官方有很好的教学资料与源码，本文主要记录学习资源与基本用法。
可以直接查阅官方教程（https://www.rabbitmq.com/getstarted.html）
结合源码进行学习。
> *本文样例源码是将官方源码（https://github.com/rabbitmq/rabbitmq-tutorials/tree/master/dotnet） 中.NET部分提取下来*
> 
## 1. 参考资料
> 官网 https://www.rabbitmq.com/
> 官方教程 https://www.rabbitmq.com/getstarted.html
> 官方教程源码 https://github.com/rabbitmq/rabbitmq-tutorials/tree/master/dotnet
> 博客 https://www.cnblogs.com/sgh1023/p/11217017.html
## 2.核心内容

 
- #### 简单发送与接收

```
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
```

## 3.样例源码地址
调试Demo可以先部署RabbitMQ服务，再进行调试
 > https://github.com/Impartsoft/Bins/tree/main/RabbitMQDemo
 
---


> 欢迎大家批评指正，共同学习，共同进步！
> 作者：Iannnnnnnnnnnnn
> 出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
> 本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。