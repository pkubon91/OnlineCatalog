namespace Business.Administration
{
    public class User : BaseDomainObject
    {
        internal User()
        {
            
        }

        public User(bool isAdminMode) : this()
        {
            IsAdminMode = isAdminMode;
        }

        public User(string login, string password, bool isAdminUser) : this(isAdminUser)
        {
            IsAdminMode = isAdminUser;
            Login = login;
            Password = password;
        }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual bool IsAdminMode { get; private set; }

        public virtual UserAddress Address { get; set; }

        public virtual string Login { get; private set; }

        public virtual string Password { get; private set; }
    }
}
