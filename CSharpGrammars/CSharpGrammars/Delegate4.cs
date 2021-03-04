using System;

namespace CSharpGrammars
{
    class DelegateTest4
    {
        public void MainSayHello()
        {
            var geet = new Geet();
            geet.getHello += Test;
            geet.getHello += GetCnHello;

            geet.SayHello("Harry");

            geet.getHello();

        }

        private string Test()
        {
            Console.WriteLine("Test");

            return "Test";
        }

        private string GetCnHello()
        {
            return "Hello";
        }
    }


    class Geet
    {
        public delegate string DelegateGetHello();
        public DelegateGetHello getHello;

        public void SayHello(string name)
        {
            Console.WriteLine(getHello() + name);
        }
    }
}
