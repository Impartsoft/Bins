using DinkToPdf;
using System;
using System.IO;

namespace DinkToPdfDemo
{
    public static class HtmlToPdfDocumentExtensions
    {
        /// <summary>
        /// 导出PDF文件
        /// </summary>
        public static void ExportPDF(this HtmlToPdfDocument doc)
        {
            var converter = new BasicConverter(new PdfTools());

            byte[] pdf = converter.Convert(doc);

            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            using (FileStream stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }
        }
    }
}