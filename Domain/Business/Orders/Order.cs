using System;
using System.Security.Permissions;
using Business.Administration;

namespace Business.Orders
{
    public class Order : BaseDomainObject, IAuditable
    {
        internal Order()
        { }

        public Order(Basket basket) : this()
        {
            
        }

        public User Seller { get; set; }

        public Basket BasketToOrder { get; protected set; }

        public decimal PercentDiscount { get; set; }

        public decimal FinalPrice { get; protected set; }

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
