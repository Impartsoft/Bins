using System;
using System.Threading;

namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过匿名委托创建
            Thread thread1 = new Thread(delegate () { Console.WriteLine("我是通过匿名委托创建的线程"); });
            thread1.Start();
            //通过Lambda表达式创建
            Thread thread2 = new Thread(() => Console.WriteLine("我是通过Lambda表达式创建的委托"));
            thread2.Start();
            Console.ReadKey();
        }
    }
}
