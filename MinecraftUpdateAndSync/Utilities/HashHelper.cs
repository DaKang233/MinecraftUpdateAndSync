using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Utilities
{
    public class HashHelper
    {
        public static string ComputeFileHash(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                System.Console.WriteLine("File path is null or empty.");
                throw new ArgumentNullException();
            }
            if (!System.IO.File.Exists(filePath))
            {
                System.Console.WriteLine($"File '{filePath}' does not exist.");
                throw new System.IO.FileNotFoundException($"File '{filePath}' not found.");
            }
            try
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error computing hash for file '{filePath}': {ex.Message}");
                return null;
            }
        }
    }
}
