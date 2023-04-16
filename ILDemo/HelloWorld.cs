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
            var assemblyType = assemblyModule.DefineType("HelloWorld", TypeAttributes.Public);
            var methodBuilder = assemblyType.DefineMethod("Print", MethodAttributes.Public | MethodAttributes.Static);
            var il= methodBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldstr, "HelloWorld");
            il.Emit(OpCodes.Call,METHOD_INFO);
            il.Emit(OpCodes.Ret);

            var delegate1 = dm.CreateDelegate(typeof(Action));
            delegate1.DynamicInvoke();
        }

    }
}
