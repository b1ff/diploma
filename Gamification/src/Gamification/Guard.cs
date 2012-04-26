using System;
using Gamification.Core.Extensions;

namespace Gamification.Core
{
    public static class Guard
    {
        public static void ArgumentNotNull(object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }

        public static void StringArgumentIsNullOrBlank(string arg, string paramName)
        {
            if (arg.IsNullOrBlank())
                throw new ArgumentNullException(paramName);
        }
    }
}
