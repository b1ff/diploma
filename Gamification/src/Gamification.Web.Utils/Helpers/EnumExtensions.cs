using System;

namespace Gamification.Web.Utils.Helpers
{
    public static class EnumExtensions
    {
        public static byte Val<TEnum>(this TEnum val)
            where TEnum : struct 
        {
            return Convert.ToByte(val);
        }
    }
}
