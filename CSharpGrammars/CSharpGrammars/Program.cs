using System;
using System.Threading.Tasks;

namespace CSharpGrammars
{
    // CSharp语法官方介绍 https://docs.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/features
    // 委托博客 https://www.cnblogs.com/SkySoot/archive/2012/04/05/2433639.html
    // sharplab https://sharplab.io/#v2:CYLg1APgBFACBMUCCBnAngOwMYBUCmKALgLABQMMA3mRbTLAIwBscALMutgBQCUNdVAL79BQA===
    // 异步编程  https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/
    class Program
    {
        static async Task Main(string[] args)
        {
            //var s = new DelegateTest1();
            //s.SayHello();

            //var s2 = new DelegateTest2();
            //s2.SayHello();
            //s2.SayHello2();

            //var s3 = new DelegateTest3();
            //s3.SayHello();

            //var s4 = new DelegateTest4();
            //s4.MainSayHello();

            await AsyncTest.Async();

            BoxingAndUnboxing();

            var p = new Point(10, 20);
            var p1 = p.Point2(0, 0);

            var sp = new StructPoint(11, 22);
            var sp2 = new StructPoint(11, 22);
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
