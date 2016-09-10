using System;

namespace OnlineCatalog.Services.SessionManagerService
{
    public class UtcDateTimeProvider : IUtcDateTimeProvider
    {
        public DateTime UtcDateTimeNow => DateTime.UtcNow;
    }
}