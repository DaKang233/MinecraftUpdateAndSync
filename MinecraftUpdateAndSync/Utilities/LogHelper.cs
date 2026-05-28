using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Utilities
{
    public class LogHelper
    {
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        public enum LogDebugLevel
        {
            None,
            Low,
            Medium,
            High
        }

        public static LogDebugLevel ApplicationDebugLevel { get; set; } = LogDebugLevel.None;

        private static readonly string LogFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "logs",
                $"log-{DateTime.Now:yyyy-MM-dd}.txt");

        // private static StreamWriter _writer;

        private static string GetLogLevelString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARNING";
                case LogLevel.Error:
                    return "ERROR";
                default:
                    return "UNKNOWN";
            }
        }

        private static void Log(
            LogLevel level,
            string message,
            LogDebugLevel debugLevel = LogDebugLevel.None,
            string memberName = "",
            string filePath = ""
        )
        {
            if (debugLevel <= ApplicationDebugLevel)
            {
                var className = Path.GetFileNameWithoutExtension(filePath);

                string logMessage =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                    $"[{GetLogLevelString(level)}] " +
                    $"[{className}.{memberName}] {message}";
                
                Console.WriteLine( logMessage );
                WriteToFile( logMessage );
            }
        }

        private static void WriteToFile(string message)
        {
            try
            {
                string logDirectory =
                    Path.GetDirectoryName(LogFilePath);

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                File.AppendAllText(
                    LogFilePath,
                    message + Environment.NewLine);
            }
            catch
            {
                // 日志系统不要因为写日志失败而崩溃
            }
        }

        public static void LogWarning(
            string message, 
            LogDebugLevel debugLevel, 
            [CallerMemberName] string memberName = "", 
            [CallerFilePath] string filePath = "")
        {
            Log(LogLevel.Warning, message, debugLevel, memberName, filePath);
        }

        public static void LogError(
            string message,
            LogDebugLevel debugLevel,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "")
        {
            Log(LogLevel.Error, message, debugLevel, memberName, filePath);
        }

        public static void LogInfo(
            string message,
            LogDebugLevel debugLevel,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "")
        {
            Log(LogLevel.Info, message, debugLevel, memberName, filePath);
        }
    }
}
