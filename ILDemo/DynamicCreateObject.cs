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
        public void Test()
        {
            ModuleBuilder mymodule = AssemblyBuilder.DefineDynamicAssembly(new System.Reflection.AssemblyName("Test"), AssemblyBuilderAccess.Run).DefineDynamicModule(nameof(mymodule));

            var type = mymodule.DefineType("MyType");
            type.DefineField();
        }
    }
}
