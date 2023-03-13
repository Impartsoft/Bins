using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDemo
{
    internal class DynamicCreateObject
    {
        public object Test()
        {
            ModuleBuilder mymodule = AssemblyBuilder.DefineDynamicAssembly(new System.Reflection.AssemblyName("Test"), AssemblyBuilderAccess.Run).DefineDynamicModule(nameof(mymodule));

            var typeBuilder = mymodule.DefineType("MyType");
            typeBuilder.DefineField("Id", typeof(int), System.Reflection.FieldAttributes.Public);
            typeBuilder.DefineField("Name", typeof(string), System.Reflection.FieldAttributes.Public);

            var methodIL = typeBuilder.DefineMethod("ToJson", System.Reflection.MethodAttributes.Public, typeof(string), Type.EmptyTypes);
            var methodGenerator = methodIL.GetILGenerator();
            methodGenerator.Emit(OpCodes.Ldarg_0);

            return Activator.CreateInstance(typeBuilder.CreateType());
        }
    }
}
