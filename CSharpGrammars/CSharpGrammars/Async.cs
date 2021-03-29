
using System;
using System.Threading.Tasks;

namespace CSharpGrammars
{
    class AsyncTest
    {
        internal async static Task Async()
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Egg> eggs =  FryEggs(2);
            Task toast =  ToastBread(2);

            await Task.WhenAll(eggs,toast);

            Console.WriteLine("eggs are ready");

            Bacon bacon = await FryBacon(3);
            Console.WriteLine("bacon is ready");

       
            Console.WriteLine("toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private async static Task ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");
            var toast = new Toast();

            ApplyButter(toast);
            ApplyJam(toast);
        }

        private async static Task<Bacon> FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private async static Task<Egg> FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }

    internal class Juice
    {
        public Juice()
        {
        }
    }

    internal class Toast
    {
    }

    internal class Bacon
    {
        public Bacon()
        {
        }
    }

    internal class Egg
    {
    }

    internal class Coffee
    {
    }
}
