using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Utilities
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

        public class ProcessMessage
        {
            public LogLevel Level { get; set; }

            public string Message { get; set; } = "";
            public string MemberName { get; set; } = "";
            public string FilePath { get; set; } = "";
            public int LineNumber { get; set; } = 0;
        }

        /// <summary>
        /// Reports a process message to the specified progress handler and logs the message with the given log level.
        /// </summary>
        /// <remarks>This method is typically used to report progress or status updates during processing,
        /// while also logging the message for diagnostic purposes. Caller information is automatically captured to aid
        /// in tracing the origin of the message.</remarks>
        /// <param name="progress">The progress handler that receives the process message. If null, the message is not reported via progress.</param>
        /// <param name="message">The message text to report and log. Cannot be null.</param>
        /// <param name="level">The severity level of the message. The default is LogLevel.Info.</param>
        /// <param name="debugLevel">The debug level of the message. The default is LogDebugLevel.None.</param>
        /// <param name="memberName">The name of the calling member. This value is automatically provided by the compiler and should not be set
        /// explicitly in most cases.</param>
        /// <param name="filePath">The full file path of the source file containing the caller. This value is automatically provided by the
        /// compiler.</param>
        /// <param name="lineNumber">The line number in the source file at which the method is called. This value is automatically provided by
        /// the compiler.</param>
        public static void Report(
            IProgress<ProcessMessage> progress,
            string message,
            LogLevel level = LogLevel.Info,
            LogDebugLevel debugLevel = LogDebugLevel.None,

            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var msg = new ProcessMessage
            {
                Message = message,
                Level = level,
                MemberName = memberName,
                FilePath = Path.GetFileNameWithoutExtension(filePath),
                LineNumber = lineNumber
            };

            progress?.Report(msg);

            // 自动写日志
            Log(
                level,
                message,
                debugLevel,
                memberName,
                filePath,
                lineNumber);
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
            string filePath = "",
            int lineNumber = 0
        )
        {
            if (debugLevel <= ApplicationDebugLevel)
            {
                var className = Path.GetFileNameWithoutExtension(filePath);

                string logMessage =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                    $"[{GetLogLevelString(level)}] " +
                    $"[{className}.{memberName}:{lineNumber}] {message}";
                
                Console.WriteLine( logMessage );
                WriteToFile( logMessage );
            }
        }

        private static void WriteToFile(string message)
        {
            try
            {
                string? logDirectory =
                    Path.GetDirectoryName(LogFilePath);

                if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
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
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Warning, message, debugLevel, memberName, filePath, lineNumber);
        }

        public static void LogError(
            string message,
            LogDebugLevel debugLevel,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Error, message, debugLevel, memberName, filePath, lineNumber);
        }

        public static void LogInfo(
            string message,
            LogDebugLevel debugLevel,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Info, message, debugLevel, memberName, filePath, lineNumber);
        }
    }
}
