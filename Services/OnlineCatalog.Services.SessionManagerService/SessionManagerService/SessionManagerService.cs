using System;
using System.ServiceModel;

namespace OnlineCatalog.Services.SessionManagerService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, InstanceContextMode = InstanceContextMode.Single)]
    public class SessionManagerService : ISessionManagerService
    {
        private readonly IActiveUsers _activeUsers;
        private readonly IUtcDateTimeProvider _utcDateTimeProvider;

        protected SessionManagerService()
        {
            
        }

        public SessionManagerService(IActiveUsers activeUsers, IUtcDateTimeProvider utcDateTimeProvider) : this()
        {
            if (activeUsers == null) throw new ArgumentNullException(nameof(activeUsers));
            if (utcDateTimeProvider == null) throw new ArgumentNullException(nameof(utcDateTimeProvider));

            _activeUsers = activeUsers;
            _utcDateTimeProvider = utcDateTimeProvider;
        }
         
        public bool IsUserActive(string userName)
        {
            if (_activeUsers.Users.ContainsKey(userName))
            {
                DateTime lastActivity = _activeUsers[userName];
                if (lastActivity.AddMinutes(10) >= _utcDateTimeProvider.UtcDateTimeNow)
                {
                    ActivateUser(userName);
                    return true;
                }
                _activeUsers.Users.Remove(userName);
            }
            return false;
        }

        public void ActivateUser(string userName)
        {
            _activeUsers[userName] = _utcDateTimeProvider.UtcDateTimeNow;
        }
    }
}