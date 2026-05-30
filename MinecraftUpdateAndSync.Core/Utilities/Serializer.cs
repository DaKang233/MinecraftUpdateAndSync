using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MinecraftUpdateAndSync.Core.Utilities
{
    public class Serializer
    {
        public static void SaveToFile<T>(string filePath, T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Deserializes an object of type T from a JSON file at the specified path.
        /// </summary>
        /// <remarks>The method expects the file to contain valid JSON representing an object of type T.
        /// If the file content is not valid JSON or does not match the expected type, a deserialization exception may
        /// occur.</remarks>
        /// <typeparam name="T">The type of the object to deserialize. Must be a reference type.</typeparam>
        /// <param name="filePath">The path to the JSON file containing the serialized object. Cannot be null or empty.</param>
        /// <returns>An instance of type T deserialized from the specified file.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file specified by filePath does not exist.</exception>
        public static T LoadFromFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"加载文件失败：{filePath}");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json) ?? throw new ArgumentException("解析出错。");
        }
        public static string SerializeToString<T>(T obj, JsonSerializerSettings? settings = null)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
        public static T DeserializeFromString<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json) ?? throw new ArgumentException("解析出错。");
        }
    }
}

