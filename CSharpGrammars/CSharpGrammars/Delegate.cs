using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpGrammars
{
    delegate double Function2(double x);

    class Multiplier
    {
        double _factor;

        public Multiplier(double factor) => _factor = factor;

        public double Multiply(double x) => x * _factor;
    }

    class DelegateExample
    {
        static double[] Apply(double[] a, Function2 f)
        {
            var result = new double[a.Length];
            for (int i = 0; i < a.Length; i++) result[i] = f(a[i]);
            return result;
        }

        public static void Main2()
        {
            double[] a = { 0.0, 0.5, 1.0 };
            double[] squares = Apply(a, (x) => x * x);
            double[] sines = Apply(a, Math.Sin);
            Multiplier m = new Multiplier(2.0);
            double[] doubles = Apply(a, m.Multiply);
        }
    }
}
