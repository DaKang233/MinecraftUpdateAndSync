using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Utilities
{
    public class PathHelper
    {
        public static string GetRelativePath(string rootPath, string fullPath)
        {
            Uri rootUri = new Uri(rootPath.EndsWith("\\")
                ? rootPath
                : rootPath + "\\");

            Uri fileUri = new Uri(fullPath);

            return Uri.UnescapeDataString(
                rootUri.MakeRelativeUri(fileUri)
                    .ToString()
                    .Replace('/', '\\'));
        }
    }
}
