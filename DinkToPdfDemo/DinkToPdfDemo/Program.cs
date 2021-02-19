using DinkToPdf;
using System;

namespace DinkToPdfDemo
{
    /// <summary>
    /// .NET使用DinkToPdf将HTML转成PDF
    /// 作者：Iannnnnnnnnnnnn
    /// 博客：https://www.cnblogs.com/Iannnnnnnnnnnnn/p/14375513.html
    public class Program
    {
        public static void Main(string[] args)
        {
            string html = @"<!DOCTYPE html>
<html>
<head> 
<meta charset='utf-8'> 
<title>W3Cschool</title> 
</head>
<body>

<h4>水平标题:</h4>
<table border='1'>
<tr>
  <th>Name</th>
  <th>Telephone</th>
  <th>Telephone</th>
</tr>
<tr>
  <td>Bill Gates</td>
  <td>555 77 854</td>
  <td>555 77 855</td>
</tr>
</table>

<h4>垂直标题:</h4>
<table border='1'>
<tr>
  <th>First Name:</th>
  <td>Bill Gates</td>
</tr>
<tr>
  <th>Telephone:</th>
  <td>555 77 854</td>
</tr>
<tr>
  <th>Telephone:</th>
  <td>555 77 855</td>
</tr>
</table>

</body>
</html>";
            ExportHtmlContent(html);

            ExportWebPage("https://www.baidu.com/");

            Console.ReadKey();
        }

        /// <summary>
        /// 导出指定网页
        /// </summary>
        private static void ExportWebPage(string url)
        {
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

            doc.ExportPDF();
        }

        /// <summary>
        /// 导出HTML文本
        /// </summary>
        private static void ExportHtmlContent(string htmlContent)
        {
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

            doc.ExportPDF();
        }
    }
}
