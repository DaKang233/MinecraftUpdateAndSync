using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Utilities
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
    }
}
