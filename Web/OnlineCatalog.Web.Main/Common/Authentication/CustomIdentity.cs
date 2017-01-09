using System.Security.Principal;
using Castle.Core.Internal;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public string AuthenticationType => "Forms";

        public bool IsAuthenticated => !Name.IsNullOrEmpty();
    }
}