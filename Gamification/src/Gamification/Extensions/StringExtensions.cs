namespace Gamification.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrBlank(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        public static bool IsNotNullOrBlank(this string @string)
        {
            return !string.IsNullOrWhiteSpace(@string);
        }

        public static string SafeToString(this object @object)
        {
            return @object == null ? string.Empty : @object.ToString();
        }
    }
}
