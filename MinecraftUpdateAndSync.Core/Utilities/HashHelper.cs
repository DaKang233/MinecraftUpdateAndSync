using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Utilities
{
    public class HashHelper
    {
        /// <summary>
        /// Attempts to compute the SHA-256 hash of the specified file and returns a value indicating whether the
        /// operation succeeded.
        /// </summary>
        /// <remarks>This method returns false if the file does not exist, is inaccessible, or an I/O
        /// error occurs. The method does not throw exceptions for common file access errors, but logs them internally.
        /// Only errors during hash conversion result in an exception.</remarks>
        /// <param name="filePath">The path to the file for which to compute the hash. Cannot be null or empty. The file must exist and be
        /// accessible.</param>
        /// <param name="hash">When this method returns, contains the hexadecimal string representation of the file's SHA-256 hash if the
        /// operation succeeds; otherwise, null.</param>
        /// <returns>true if the hash was computed successfully; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">Thrown if an error occurs while converting the computed hash bytes to a string.</exception>
        public static bool TryComputeFileHash(string filePath, out string hash)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                LogHelper.LogError("File path is null or empty.", LogHelper.LogDebugLevel.Low);
                hash = string.Empty;
                return false;
            }
            if (!System.IO.File.Exists(filePath))
            {
                LogHelper.LogError($"File does not exist at path '{filePath}'.", LogHelper.LogDebugLevel.Low);
                hash = string.Empty;
                return false;
            }
            try
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var hashBytes = sha256.ComputeHash(stream);
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    return true;
                }
            }
            catch (System.IO.IOException ioEx)
            {
                LogHelper.LogError($"I/O error while reading file '{filePath}': {ioEx.Message}", LogHelper.LogDebugLevel.Low);
                hash = "";
                return false;
            }
            catch (UnauthorizedAccessException accEx)
            {
                LogHelper.LogError($"Access denied while reading file '{filePath}': {accEx.Message}", LogHelper.LogDebugLevel.Low);
                hash = "";
                return false;
            }
            catch (NullReferenceException nfEx)
            {
                throw new InvalidOperationException("Error converting hash bytes to string.", nfEx);
            }
            catch (ArgumentOutOfRangeException aoEx)
            {
                throw new InvalidOperationException("Error converting hash bytes to string.", aoEx);
            }
            catch (CryptographicException ex)
            {
                throw new InvalidOperationException("Error computing hash for file.", ex);
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error computing hash for file '{filePath}': {ex.Message}", LogHelper.LogDebugLevel.Low);
                hash = "";
                return false;
            }
        }
    }
}

