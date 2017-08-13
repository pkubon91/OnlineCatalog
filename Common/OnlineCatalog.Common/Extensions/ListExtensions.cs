using System;
using System.Linq;

namespace OnlineCatalog.Common.Extensions
{
    public static class ListExtensions
    {
        public static bool IsIn<T>(this T item, params T[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            return items.Any(t => t.Equals(item));
        }
    }
}
