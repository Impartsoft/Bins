using System;
using System.ComponentModel;

namespace EnumDemo
{
    class Program
    {
        /// <summary>
        /// C#关于枚举使用一些总结以及扩展类的分享
        /// 作者：Iannnnnnnnnnnnn
        /// 博客：https://www.cnblogs.com/Iannnnnnnnnnnnnhttps://www.cnblogs.com/Iannnnnnnnnnnnn
        /// </summary>
        static void Main(string[] args)
        {
            EnumBasic();
            EnumExtensions();

            Console.ReadKey();
        }

        private static void EnumBasic()
        {
            // 枚举 => 转字符串
            string yesString = YesOrNo.Yes.ToString(); // Yes
            // 枚举 => 转数字
            int yesInt = (int)YesOrNo.Yes; // 1

            // 字符串 => 枚举
            YesOrNo yesOrNo_Yes = (YesOrNo)Enum.Parse(typeof(YesOrNo), "Yes"); // YesOrNo.Yes
            // 数字 => 枚举
            YesOrNo yesOrNo_No = (YesOrNo)2; // YesOrNo.No

            // 获取所有的枚举成员
            Array yesOrNos = Enum.GetValues(typeof(YesOrNo)); // [YesOrNo.None,YesOrNo.Yes,YesOrNo.No]
            // 获取所有枚举成员的字段名
            string[] yesOrNoNames = Enum.GetNames(typeof(YesOrNo)); // ["None","Yes","No"]

            // 获取枚举所属的数字类型
            Type typeInt = Enum.GetUnderlyingType(typeof(YesOrNo)); // System.Int32
            Type typeByte = Enum.GetUnderlyingType(typeof(YesOrNo_Byte)); // System.Byte
        }

        private static void EnumExtensions()
        {
            // GetEnum()  字符串 => 转枚举
            var yesString = "Yes".GetEnum<YesOrNo>(); // YesOrNo.Yes

            // GetIntValue() 枚举 => 转数字
            int yesInt = YesOrNo.Yes.GetIntValue(); // 1

            // GetDescription()  获取枚举的描述
            var description = YesOrNo.Yes.GetDescription(); // 是

            // GetEnumDescriptions() 获取枚举字符串值与描述
            var dictionary = typeof(YesOrNo).GetEnumDescriptions(); // {{[None, ""]},{[Yes, 是]},{[No, 否]}}

            // GetEnumIntDescriptions() 获取枚举值与描述
            var intDictionary = typeof(YesOrNo).GetEnumIntDescriptions(); // {{[0, ""]},{[1, 是]},{[2, 否]}}
        }
    }
}

// 枚举YesOrNo
public enum YesOrNo
{
    [Description("")]
    None = 0,
    [Description("是")]
    Yes = 1,
    [Description("否")]
    No = 2,
}

// 枚举YesOrNo 基础类型为byte
public enum YesOrNo_Byte : byte
{
    [Description("")]
    None = 0,
    [Description("是")]
    Yes = 1,
    [Description("否")]
    No = 2,
}