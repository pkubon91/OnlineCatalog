using System.Collections.Generic;
using Business.Groups;

namespace Business.Administration
{
    public class User : BaseDomainObject
    {
        internal User()
        {
            
        }

        public User(UserRank userRank) : this()
        {
            UserRank = userRank;
        }

        public User(string login, string password, UserRank userRank) : this(userRank)
        {
            Login = login;
            Password = password;
        }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual UserAddress Address { get; set; }

        public virtual string Login { get; private set; }

        public virtual string Password { get; private set; }

        public virtual UserRank UserRank { get; private set; }

        public virtual IEnumerable<Shop> AssignedShops { get; private set; } = new List<Shop>();
    }
}
