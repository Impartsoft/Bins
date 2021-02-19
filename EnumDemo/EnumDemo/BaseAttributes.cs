using System;

namespace EnumDemo
{
    public class BaseAttributes : Attribute
    {
        public string DescribeName { get; set; }
        public string EnumName { get; set; }
        public int EnumValue { get; set; }

    }
}
