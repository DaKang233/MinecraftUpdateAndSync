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
    }

    public enum FileStatus
    {
        Ignore,
        Delete,
        Missing,
        Different,
        Same
    }
}
