using System;
using System.Collections.Generic;
using Business.Administration;

namespace Business.Groups
{
    public class Shop : BaseDomainObject, IAuditable, IActiveable
    {
        public virtual string Name { get; set; }

        public virtual UserAddress Address { get; set; }

        public virtual IList<User> AssignedUsers { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetDeleted(bool isDeleted)
        {
            if (isDeleted == false)
            {
                SetActive(false);
            }
            IsDeleted = isDeleted;
        }

        public bool IsActive { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public void SetCreatedDate(DateTime dateTime)
        {
            if (Created == DateTime.MinValue)
            {
                Created = dateTime;
            }
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            Updated = dateTime;
        }

        public void AssignUser(User userToAssign)
        {
            if(userToAssign == null) throw new ArgumentNullException(nameof(userToAssign));
            AssignedUsers.Add(userToAssign);
        }

        public void UnassignUser(User userToAssign)
        {
            if (userToAssign == null) throw new ArgumentNullException(nameof(userToAssign));
            if (AssignedUsers.Contains(userToAssign)) AssignedUsers.Remove(userToAssign);
        }
    }
}
