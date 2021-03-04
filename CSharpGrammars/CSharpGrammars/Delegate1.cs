using System;

namespace CSharpGrammars
{
    // https://www.cnblogs.com/SkySoot/archive/2012/04/05/2433639.html
    class DelegateTest1
    {
        public delegate string Funcionnn();

        public string GetEnHello()
        {
            return "Hello";
        }

        public static string GetCnHello()
        {
            return "你好";
        }

        public void SayHello()
        {
            Say("harry", GetEnHello);
            Say("赵四", GetCnHello);
        }

        public void Say(string name, Funcionnn getHello)
        {
            Console.WriteLine(getHello() + name);
        }
    }
}
