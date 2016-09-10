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
            set
            {
                if (Users.ContainsKey(key))
                {
                    Users[key] = value;
                }
                else
                {
                    Users.Add(key, DateTime.UtcNow);
                }
            }
            get { return Users[key]; }
        }

        public Dictionary<string, DateTime> Users { get; }
    }
}