using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDemo
{
    class MainClass
    {
        public static int Main()
        {
            Type[] wlParams = new Type[] {typeof(int)};

            MethodInfo writeLineMI = typeof(Console).GetMethod(
                            "WriteLine",
                        wlParams);

            var dynMethod = new DynamicMethod("Metoda", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard,
                              null, Type.EmptyTypes, typeof(MainClass), true);
            var il = dynMethod.GetILGenerator();
            il.Emit(OpCodes.Ldc_I4_7);

            il.Emit(OpCodes.Call, writeLineMI);
         
            il.Emit(OpCodes.Ret);

            var deleg = (Action)dynMethod.CreateDelegate(typeof(Action));
            deleg();
            return 0;
        }
    }
}
