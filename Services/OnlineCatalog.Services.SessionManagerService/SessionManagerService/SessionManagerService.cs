using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace OnlineCatalog.Services.SessionManagerService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, InstanceContextMode = InstanceContextMode.Single)]
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
                ActiveUsers.Remove(userName);
            }
            return false;
        }

        public void ActivateUser(string userName)
        {
            if (ActiveUsers.ContainsKey(userName))
            {
                ActiveUsers[userName] = DateTime.UtcNow;
            }
            else
            {
                ActiveUsers.Add(userName, DateTime.UtcNow);
            }
        }
    }
}