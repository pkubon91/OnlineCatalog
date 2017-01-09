﻿using System;
using System.Collections.Generic;
using Business.Administration;

namespace Business.Groups
{
    public class Shop : BaseDomainObject, IAuditable, IActiveable
    {
        public virtual string Name { get; set; }

        public virtual UserAddress Address { get; set; }

        public virtual IEnumerable<User> AssignedUsers { get; set; }

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
    }
}
