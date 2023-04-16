using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace ILDemo
{
    internal class Practise
    {
        static MethodInfo METHOD_INFO = typeof(Console).GetMethod(nameof(Console.Write), new Type[] { typeof(string) });

        public void SayHello()
        {
            Console.WriteLine("HelloWord");
        }

        public static void DynamicMethodMyTest1()
        {
            var dm = new DynamicMethod("HelloWord", null, null);
            var il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "HelloWorld");
            //il.Emit(OpCodes.Call,METHOD_INFO);
            il.EmitCall(OpCodes.Call, METHOD_INFO, null);

            il.Emit(OpCodes.Ret);

            var delegate1 = dm.CreateDelegate(typeof(Action));
            delegate1.DynamicInvoke();
        }

        public static void ExpressionMyTest1()
        {
            var value = Expression.Constant("HelloWorld");
            var eb = Expression.Call(METHOD_INFO, value);
            var ep = Expression.Lambda<Action>(eb);

            var delegate2 = ep.Compile();
            delegate2.DynamicInvoke();
        }
    }
}
