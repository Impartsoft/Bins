using System.Collections.Generic;
using System.Linq;

namespace ThreadDemo
{
    public class File
    {
        public string Path { get; set; }
        public IEnumerable<File> Children { get; set; }
    }

    public class File2 : File
    {
    }

    public class File3 : File
    {
    }

    public static class FileExtentions
    {
        public static IEnumerable<File> GetFiles(this File file)
        {
            if (file.Children == null || !file.Children.Any())
                yield return file;

            foreach (var c in file.Children)
            {
                c.GetFiles();
            }
        }
    }
}
