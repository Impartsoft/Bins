using System;

namespace CSharpGrammars
{
    class DelegateTest2
    {
        public delegate string GetHello();

        public void SayHello()
        {
            GetHello delegate1;
            delegate1 = GetEnHello;
            delegate1 += GetCnHello;

            Say("harry", delegate1);
        }

        public void SayHello2()
        {
            GetHello delegate1;
            delegate1 = GetEnHello;
            delegate1 += GetCnHello;

            delegate1();
        }

        public string GetEnHello()
        {
            return "Hello";
        }

        public static string GetCnHello()
        {
            return "你好";
        }

        public void Say(string name, GetHello getHello)
        {
            Console.WriteLine(getHello() + name);
        }
    }
}
