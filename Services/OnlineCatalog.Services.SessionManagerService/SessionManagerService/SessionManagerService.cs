using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace OnlineCatalog.Services.SessionManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = false, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SessionManagerService : ISessionManagerService
    {
        private Dictionary<string, DateTime> ActiveUsers { get; set; } = new Dictionary<string, DateTime>();
         
        public bool IsUserActive(string userName)
        {
            if (ActiveUsers.ContainsKey(userName))
            {
                DateTime lastActivity = ActiveUsers[userName];
                if (lastActivity.AddMinutes(10) >= DateTime.UtcNow)
                {
                    ActivateUser(userName);
                    return true;
                }
            }
            return false;
        }

        public void ActivateUser(string userName)
        {
            if (ActiveUsers.ContainsKey(userName))
            {
                ActiveUsers[userName] = DateTime.UtcNow;   
            }
            ActiveUsers.Add(userName, DateTime.UtcNow);
        }
    }
}