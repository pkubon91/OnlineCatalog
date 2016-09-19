using System;
using System.Collections.Generic;
using Business.Administration;

namespace Business.Groups
{
    public class Shop : BaseDomainObject, IAuditable, IActiveable
    {
        private DateTime _created;

        private DateTime _updated;

        private bool _isActive;

        private bool _isDeleted;

        public virtual string Name { get; set; }

        public virtual UserAddress Address { get; set; }

        public virtual IEnumerable<User> AssignedUsers { get; set; }

        public DateTime Created => _created;

        public DateTime Updated => _updated;

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void SetDeleted(bool isDeleted)
        {
            if (isDeleted == false)
            {
                SetActive(false);
            }
            _isDeleted = isDeleted;
        }

        public bool IsActive => _isActive;

        public bool IsDeleted => _isDeleted;

        public void SetCreatedDate(DateTime dateTime)
        {
            if (Created == DateTime.MinValue)
            {
                _created = dateTime;
            }
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            _updated = dateTime;
        }
    }
}
