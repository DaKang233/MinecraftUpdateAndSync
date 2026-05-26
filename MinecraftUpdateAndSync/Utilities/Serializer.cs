using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MinecraftUpdateAndSync.Utilities
{
    public class Serializer
    {
        public static void Save<T>(string filePath, T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static T Load<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}