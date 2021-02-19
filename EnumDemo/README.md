



## 0.介绍
> 枚举是一组命名常量，其基础类型为任意整型。 如果没有显式声明基础类型， 则为Int32

在实际开发过程中，枚举的使用可以让代码更加清晰且优雅。

最近在对枚举的使用进行了一些总结与整理，也发现了一些很有意思的知识盲区。

接下来先简单为大家介绍枚举在开发过程中的常用内容以及扩展类的分享。如果喜欢直接看代码的可以查看最后的样例源码。

## 1. 参考资料
> 官方Doc https://docs.microsoft.com/zh-cn/dotnet/api/system.enum?view=net-5.0

> 博客 https://www.cnblogs.com/kissdodog/archive/2013/01/16/2863515.html

> 博客 https://www.cnblogs.com/willick/p/csharp-enum-superior-tactics.html

## 2.核心内容

- #### 枚举的使用心得
0.枚举数值在开发过程中一旦确定不允许更改（除非必要 ）

1.在定义枚举的时候要设置0值，且不作为有效的业务值。（不作为有效值的原因是枚举的初始化值为零，在没有正确赋值的情况下，已经有默认值可能会造成困扰，所以直接不使用0作为业务有效值，可以省去不必要的麻烦，这半点纯属个人建议~）

这一点官方文档也有“最佳做法”的建议。
> 如果未定义值为0的枚举成员，则考虑创建 None 枚举常数。 默认情况下，由公共语言运行时将用于枚举的内存初始化为零。 因此，如果未定义值为零的常量，则在创建枚举时将包含非法值。

2.在前后端交互过程中，如果后端接收的对象中包含枚举的话，需要将枚举属性定义成可空枚举，否则前端数据有可能（前端属性值在后端的枚举值中匹配不上时）无法传输到后端。

3.数据库保存枚举值而非枚举属性字符串

虽然保存枚举属性字符串会更加直观，但是不利于后续枚举字符串重命名，且字符串长度限制也制约着枚举的命名...
- #### 枚举的基本用法

##### 定义枚举
> 枚举并不显式从继承 Enum ; 继承关系由编译器隐式处理
```
// 枚举YesOrNo
public enum YesOrNo
{
    [Description("")]
    None = 0,
    [Description("是")]
    Yes = 1,
    [Description("否")]
    No = 2
}

// 枚举YesOrNo 基础类型为byte
public enum YesOrNo_Byte : byte
{
    [Description("")]
    None = 0,
    [Description("是")]
    Yes = 1,
    [Description("否")]
    No = 2
}
```

##### 枚举 => 转字符串
```
string yesString = YesOrNo.Yes.ToString(); // Yes
```

##### 枚举 => 转数字
```
int yesInt = (int)YesOrNo.Yes; // 1
```

##### 字符串 => 枚举
```
YesOrNo yesOrNo_Yes = (YesOrNo)Enum.Parse(typeof(YesOrNo), "Yes"); // YesOrNo.Yes
```

##### 数字 => 枚举
```
YesOrNo yesOrNo_No = (YesOrNo)2; // YesOrNo.No
```

##### 获取所有的枚举成员
```
Array yesOrNos = Enum.GetValues(typeof(YesOrNo)); // [YesOrNo.None,YesOrNo.Yes,YesOrNo.No]
```

##### 获取所有枚举成员的属性名
```
string[] yesOrNoNames = Enum.GetNames(typeof(YesOrNo)); // ["None","Yes","No"]
```

##### 获取枚的举基础类型
```
Type typeInt = Enum.GetUnderlyingType(typeof(YesOrNo)); // System.Int32
    
Type typeByte = Enum.GetUnderlyingType(typeof(YesOrNo_Byte)); // System.Byte
```

- #### 扩展方法

##### 字符串 => 转枚举
```
// GetEnum()  字符串 => 转枚举
var yesString = "Yes".GetEnum<YesOrNo>(); // YesOrNo.Yes

/// <summary>
/// 根据字符串转成指定枚举值
/// </summary>
public static T GetEnum<T>(this string enumString)
{
    return (T)Enum.Parse(typeof(T), enumString);
}
```
##### 枚举 => 转数字
```
// GetIntValue() 枚举 => 转数字
int yesInt = YesOrNo.Yes.GetIntValue(); // 1

/// <summary>
/// 获取枚举的值
/// </summary>
public static int GetIntValue(this Enum value)
{
    return Convert.ToInt32(value);
}            
```

##### 获取枚举的描述
```
// GetDescription()  获取枚举的描述
var description = YesOrNo.Yes.GetDescription(); // 是

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
```

##### 将枚举字符串值与描述转字典
```
// GetEnumDescriptions() 获取枚举字符串值与描述
var dictionary = typeof(YesOrNo).GetEnumDescriptions(); // {{[None, ""]},{[Yes, 是]},{[No, 否]}}

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
```

##### 将枚举值与描述转字典
```
// GetEnumIntDescriptions() 获取枚举值与描述
var intDictionary = typeof(YesOrNo).GetEnumIntDescriptions(); // {{[0, ""]},{[1, 是]},{[2, 否]}}

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
```

## 3.样例源码地址

 > https://github.com/Impartsoft/Bins/tree/main/EnumDemo



```
欢迎大家批评指正，共同学习，共同进步！
作者：Iannnnnnnnnnnnn
出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。
```
