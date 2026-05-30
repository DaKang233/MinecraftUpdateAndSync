using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Utilities
{
    public class PathHelper
    {
        public enum SlashDirection
        {
            Forward,
            Backward
        }

        /// <summary>
        /// Returns the relative path from a specified root directory to a target file or directory path, using the
        /// specified slash direction.
        /// </summary>
        /// <remarks>If the root and target paths are identical, an empty string is returned. The method
        /// does not validate whether the paths exist on the file system. The result is not prefixed with a
        /// slash.</remarks>
        /// <param name="rootPath">The absolute path to the root directory from which the relative path is calculated. Must be a valid file
        /// system path.</param>
        /// <param name="fullPath">The absolute path to the target file or directory for which the relative path is to be determined. Must be a
        /// valid file system path.</param>
        /// <param name="slashDirection">Specifies the direction of slashes to use in the resulting relative path. Use SlashDirection.Forward for
        /// forward slashes ('/'), or SlashDirection.Backward for backward slashes ('\'). The default is
        /// SlashDirection.Forward.</param>
        /// <returns>A relative path from the root directory to the target path, formatted with the specified slash direction.</returns>
        public static string GetRelativePath(string rootPath, string fullPath, SlashDirection slashDirection = SlashDirection.Forward)
        {
            Uri rootUri = new Uri(rootPath.EndsWith("\\")
                ? rootPath
                : rootPath + "\\");

            Uri fileUri = new Uri(fullPath);
            if (slashDirection == SlashDirection.Forward)
            {
                return Uri.UnescapeDataString(
                    rootUri.MakeRelativeUri(fileUri)
                        .ToString()
                        .Replace('\\', '/'));
            }
            else
            return Uri.UnescapeDataString(
                rootUri.MakeRelativeUri(fileUri)
                    .ToString()
                    .Replace('/', '\\'));
        }

        /// <summary>
        /// Converts all directory separator characters in the specified path to forward slashes ('/').
        /// </summary>
        /// <remarks>This method does not validate the existence of the path or modify any other path
        /// components. It is useful for standardizing paths for comparison or storage.</remarks>
        /// <param name="path">The file or directory path to normalize. Cannot be null.</param>
        /// <returns>A string representing the normalized path with all directory separators as forward slashes.</returns>
        public static string GetNormalizedPath(string path)
        {
            return path.Replace('\\','/');
        }
    }
}

