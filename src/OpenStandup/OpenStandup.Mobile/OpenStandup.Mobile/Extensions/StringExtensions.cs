

namespace OpenStandup.Mobile.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength = 100, bool ellipsis = false)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : $"{value.Substring(0, maxLength)}{(ellipsis ? "..." : "")}";
        }
    }
}
