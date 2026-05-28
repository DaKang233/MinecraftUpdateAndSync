using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Models
{
    /// <summary>
    /// Represents a file entry in a manifest, including its relative path, hash, size, modification time, and deletion
    /// status.
    /// </summary>
    /// <remarks>This class is typically used to track files for deployment, synchronization, or integrity
    /// verification scenarios. Each instance describes a single file and its relevant metadata as recorded in the
    /// manifest.</remarks>
    public class ManifestFile
    {
        /// <summary>
        /// 获取或设置与根目录相关的相对路径。
        /// </summary>
        public string RelativePath { get; set; }        // 相对路径
        /// <summary>
        /// 获取或设置文件的 SHA256 校验值。
        /// </summary>
        public string Hash { get; set; }        // SHA256 校验
        /// <summary>
        /// 获取或设置文件的大小（以字节为单位）。
        /// </summary>
        public long Size { get; set; }          // 文件大小（字节）
        /// <summary>
        /// 获取或设置文件的最后修改时间。
        /// </summary>
        public DateTime MTime { get; set; }        // 修改时间
    }

    /// <summary>
    /// 表示包含文件清单及其版本信息的对象。用于描述一组相关文件及其元数据。
    /// </summary>
    /// <remarks>通常用于打包、部署或版本管理场景，以便跟踪文件集合的内容和版本。</remarks>
    public class Manifest
    {
        /// <summary>
        /// Gets or sets the version identifier for the current instance.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Gets or sets the collection of manifest files associated with this instance.
        /// </summary>
        public List<ManifestFile> Files { get; set; } = new List<ManifestFile>();

        /// <summary>
        /// Compares two version strings and determines whether the first version is greater than the second version.
        /// </summary>
        /// <remarks>Both version strings must be valid and parseable by the Version class. If either
        /// string is not in a valid format, a FormatException may be thrown.</remarks>
        /// <param name="version1">The first version string to compare. Must be in a format recognized by the Version class, such as '1.2.3'.</param>
        /// <param name="version2">The second version string to compare. Must be in a format recognized by the Version class, such as '1.2.3'.</param>
        /// <returns>true if the first version is greater than the second version; otherwise, false.</returns>
        public static bool CompareVersions(string version1, string version2)
        {
            /*
            var v1 = version1.Split('.');
            var v2 = version2.Split('.');

            int maxLength = Math.Max(v1.Length, v2.Length);

            for (int i = 0; i < maxLength; i++)
            {
                int n1 = i < v1.Length ? int.Parse(v1[i]) : 0;
                int n2 = i < v2.Length ? int.Parse(v2[i]) : 0;

                if (n1 > n2)
                    return true;

                if (n1 < n2)
                    return false;
            }

            // 完全相等
            return false;
            */
            return new Version(version1) > new Version(version2);
        }
    }

    public enum FileState
    {
        Same,
        MissingLocal,
        MissingRemote,
        Different,
        InvalidLocal,
        InvalidRemote,
        Ignored
    }

    public enum SyncAction
    {
        None,
        Download,
        Replace,
        Delete,
        Ignore
    }
}
