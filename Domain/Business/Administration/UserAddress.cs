using System;

namespace Business.Administration
{
    public class UserAddress
    {
        public virtual  Guid AddressGuid { get; set; }

//        public virtual User AssociatedUser { get; set; }
        
        public virtual string City { get; set; }

        public virtual string Street { get; set; }

        public virtual string BuildingNumber { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Email { get; set; }
    }
}
