using System;

namespace CSharpGrammars
{
    // https://docs.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/features
    class Program
    {
        static void Main(string[] args)
        {
            //var s = new DelegateTest1();
            //s.SayHello();

            //var s2 = new DelegateTest2();
            //s2.SayHello();
            //s2.SayHello2();

            var s3 = new DelegateTest3();
            s3.SayHello();

            BoxingAndUnboxing();

            var p = new Point(10, 20);
            var p1 = p.Point2(0, 0);

            var sp = new StructPoint(11,22);
            var sp2 = new StructPoint(11,22);
            sp = sp2;
            
        }

        private static void BoxingAndUnboxing()
        {
            int i = 123;
            object o = i; // 装箱
            int j = (int)o; // 拆箱
        }
    }
}
