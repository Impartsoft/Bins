using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDemo
{
    public class ILAction
    {
        public void TestAction(string[] args)
        {
            Action<string> myAction = s =>
            {
                Console.WriteLine("Hello " + s);
            };
            MethodInfo method = myAction.GetMethodInfo();
            object target = myAction.Target;

            DynamicMethod dm = new DynamicMethod(
                method.Name,
                method.ReturnType,
                new[] { method.DeclaringType }.
                    Concat(method.GetParameters().
                        Select(pi => pi.ParameterType)).ToArray(),
                method.DeclaringType,
                skipVisibility: true);

            DynamicILInfo ilInfo = dm.GetDynamicILInfo();
            var body = method.GetMethodBody();
            SignatureHelper sig = SignatureHelper.GetLocalVarSigHelper();
            foreach (LocalVariableInfo lvi in body.LocalVariables)
            {
                sig.AddArgument(lvi.LocalType, lvi.IsPinned);
            }
            ilInfo.SetLocalSignature(sig.GetSignature());
            byte[] code = body.GetILAsByteArray();
            ilInfo.SetCode(code, body.MaxStackSize);

            dm.Invoke(target, new object[] { target, "World" });

            Console.ReadLine(); //Just to see the result
        }
    }
}