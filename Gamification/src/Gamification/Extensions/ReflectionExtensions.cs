using System;
using System.Linq;
using System.Reflection;

namespace Gamification.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo)
        {
            var typeofAttribute = typeof (TAttribute);
            return (TAttribute) memberInfo.GetCustomAttributes(typeofAttribute, false).FirstOrDefault();
        }
    }
}
