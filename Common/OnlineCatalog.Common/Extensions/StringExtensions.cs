using System;

namespace OnlineCatalog.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @string)
        {
            return String.IsNullOrEmpty(@string);
        }
    }
}
