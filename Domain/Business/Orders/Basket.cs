using System;
using System.Collections.Generic;
using Business.Administration;
using Business.Products;
using NHibernate.Mapping;

namespace Business.Orders
{
    public class Basket : BaseDomainObject, IAuditable
    {
        public Basket()
        {
            BasketProducts = new List<Product>();
        }

        public virtual bool IsRealized { get; set; }

        public virtual User Owner { get; set; }

        public virtual List<Product> BasketProducts { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

        public void SetCreatedDate(DateTime dateTime)
        {
            Created = dateTime;
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            Updated = dateTime;
        }
    }
}
