using System;

namespace OnlineCatalog.Services.SessionManagerService
{
    public interface IUtcDateTimeProvider
    {
        DateTime UtcDateTimeNow { get; }
    }
}
