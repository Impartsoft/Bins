



## 0.介绍
> C# .NET Core wrapper for wkhtmltopdf library that uses Webkit engine to convert HTML pages to PDF.

最近浏览文章的时候发现DinkToPdf框架，可以利用HTML转换成PDF，与我早期使用ITextSharp 框架构建PDF的方式不太一样。DinkToPdf直接将HTML转成PDF，HTML的构造直观且简单。这种方式可能可以成为不错的选择！
下面为大家简单介绍DinkToPdf的使用方式。

## 1. 参考资料
> Github https://github.com/rdvojmoc/DinkToPdf

## 2.核心内容
- #### 构造HtmlToPdfDocument对象

##### Html文本形式 
```
string html = @"<!DOCTYPE html>
<html>
<head> 
<meta charset='utf-8'> 
<title>W3Cschool</title> 
</head>
<body>

<h4>Html文本</h4>

</body>
</html>";

var doc = new HtmlToPdfDocument()
{
    GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4,
    },
    Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = htmlContent,
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]" }
        }
    }
};
```
##### URL方式
```
    
string url = ""https://www.baidu.com/";
var doc = new HtmlToPdfDocument()
{
    GlobalSettings = {
        PaperSize = PaperKind.A4Small
    },

    Objects = {
        new ObjectSettings()
        {
            Page = url,
        }
    }
};
            
```

- #### HtmlToPdfDocument 转成 PDF
``` 
var converter = new BasicConverter(new PdfTools());

byte[] pdf = converter.Convert(doc);
```

- #### 生成PDF
```
if (!Directory.Exists("Files"))
{
    Directory.CreateDirectory("Files");
}

using (FileStream stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
{
    stream.Write(pdf, 0, pdf.Length);
}
```

- #### 注意点
框架依赖于wkhtmltopdf，在自己部署的时候如果提示缺少dll，记得去官方Git上下载（样例源码已经复制了64位的dll）
> https://github.com/rdvojmoc/DinkToPdf/tree/master/v0.12.4

## 3.效果图展示

- #### Html文本形式 

##### w3c表格截图
![](https://img2020.cnblogs.com/blog/870711/202102/870711-20210204224048208-855643127.png)
##### pdf生成结果图
![](https://img2020.cnblogs.com/blog/870711/202102/870711-20210204224153501-135886120.png)

- #### URL方式
##### 百度网址生成pdf效果图
![](https://img2020.cnblogs.com/blog/870711/202102/870711-20210204224242208-628252641.png)
 

## 4.样例源码地址

 > https://github.com/Impartsoft/Bins/tree/main/DinkToPdfDemo


```
欢迎大家批评指正，共同学习，共同进步！
作者：Iannnnnnnnnnnnn
出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。
```
