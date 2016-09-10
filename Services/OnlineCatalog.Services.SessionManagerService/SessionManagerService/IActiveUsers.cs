using System;
using System.Collections.Generic;

namespace OnlineCatalog.Services.SessionManagerService
{
    public interface IActiveUsers
    {
        DateTime this[string key] { get; set; }

        Dictionary<string, DateTime> Users { get; } 
    }
}
