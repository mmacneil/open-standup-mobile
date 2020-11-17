

namespace OpenStandup.SharedKernel.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength=100)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
