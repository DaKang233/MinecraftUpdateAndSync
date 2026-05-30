using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Utilities
{
    public static class Utilities
    {
        public static Encoding TryGetGB18030Encoding()
        {
            try
            {
                return Encoding.GetEncoding("GB18030");
            }
            catch
            {
                return Encoding.UTF8;
            }
        }

        public static bool TryGetNonNullValue<TKey, TValue>(
            this Dictionary<TKey, TValue> dict,
            TKey key,
            [NotNullWhen(true)] out TValue? value)
            where TKey : notnull
            where TValue : class
        {
            if (dict.TryGetValue(key, out value) && value != null)
                return true;

            value = null;
            return false;
        }
    }
}
