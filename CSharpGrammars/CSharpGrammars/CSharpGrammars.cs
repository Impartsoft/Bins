namespace CSharpGrammars
{
    public class PointSuper
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; set; }
    }

    public interface IPoint
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; set; }
    }

    // 类的只读属性在构造函数里允许被赋！
    public class Point : PointSuper, IPoint
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; set; }

        public Point(int x, int y) => (X, Y) = (x, y);
        public int Point2(int x, int y) => Z = X;
    }

    // 接口体，简单的类型，不能声明基类。从System.ValueType隐式派生
    public struct StructPoint
    {
        public double X { get; }
        public double Y { get; }

        public StructPoint(double x, double y) => (X, Y) = (x, y);
    }
}
