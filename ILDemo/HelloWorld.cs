using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDemo
{
    internal class HelloWorld
    {
        public static void DynamicMethodMyTest1()
        {
            var assemblyName = new AssemblyName
            {
                Name = "HelloWorldAssembly"
            };

            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            var assemblyModule = assemblyBuilder.DefineDynamicModule("HelloWorldModule");
            var typeBuilder = assemblyModule.DefineType("HelloWorld", TypeAttributes.Public);
            var methodBuilder = typeBuilder.DefineMethod("Print", MethodAttributes.Public | MethodAttributes.Static);
            var il = methodBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "HelloWorld");

            var parameterTypes = new Type[1];
            parameterTypes[0] = typeof(string);

            var consoleType = Type.GetType("System.Console, System.Console", true);
            var methodInfo =  consoleType.GetMethod(nameof(Console.WriteLine),parameterTypes);

            //MethodInfo METHOD_INFO = typeof(Console).GetMethod(nameof(Console.Write), new Type[] { typeof(string) });

            il.Emit(OpCodes.Call, methodInfo);
            il.Emit(OpCodes.Ret);

            var type = typeBuilder.CreateType();
            var assembly = Assembly.GetAssembly(type!);

            type.GetMethod("Print").Invoke(null, null);

        }

    }
}
