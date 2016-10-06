using System;

namespace OnlineCatalog.Common.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmptyGuid(this Guid guid)
        {
            return guid == Guid.Empty;
        }
    }
}
