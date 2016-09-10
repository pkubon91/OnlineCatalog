using System;
using System.Collections.Generic;

namespace OnlineCatalog.Services.SessionManagerService
{
    public class ActiveUsers : IActiveUsers
    {
        public ActiveUsers()
        {
            Users = new Dictionary<string, DateTime>();
        }

        public DateTime this[string key]
        {
            set { Users.Add(key, DateTime.UtcNow); }
            get { return Users[key]; }
        }

        public Dictionary<string, DateTime> Users { get; }
    }
}