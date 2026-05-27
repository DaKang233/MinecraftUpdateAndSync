using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MinecraftUpdateAndSync.Utilities
{
    public class Serializer
    {
        public static void SaveToFile<T>(string filePath, T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static T LoadFromFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static string SerializeToString<T>(T obj, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
        public static T DeserializeFromString<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}