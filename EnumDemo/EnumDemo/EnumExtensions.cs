using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EnumDemo
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 根据字符串转成指定枚举值
        /// </summary>
        public static T GetEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        public static int GetIntValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 根据枚举获取枚举描述
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (customAttribute == null)
                return value.ToString();
            else
                return ((DescriptionAttribute)customAttribute).Description;
        }

        /// <summary>
        /// 获取枚举字符串值及描述值的字典
        /// </summary>

        public static IDictionary<string, string> GetEnumDescriptions(this Type enumType)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (Enum code in Enum.GetValues(enumType))
                dictionary.Add(code.ToString(), code.GetDescription());

            return dictionary;
        }

        /// <summary>
        /// 获取枚举值及描述值的字典
        /// </summary>

        public static IDictionary<int, string> GetEnumIntDescriptions(this Type enumType)
        {
            var dictionary = new Dictionary<int, string>();
            foreach (Enum code in Enum.GetValues(enumType))
                dictionary.Add(code.GetIntValue(), code.GetDescription());

            return dictionary;
        }
    }
}