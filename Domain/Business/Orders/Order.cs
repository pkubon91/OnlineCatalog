using System;
using Business.Administration;
using Business.Groups;

namespace Business.Orders
{
    public class Order : BaseDomainObject, IAuditable
    {
        internal Order()
        { }

        public Order(Basket basket) : this()
        {
            BasketToOrder = basket;
        }

        public virtual User Seller { get; set; }

        public virtual Basket BasketToOrder { get; protected set; }

        public virtual decimal PercentDiscount { get; set; }

        public virtual decimal FinalPrice { get; protected set; }

        public virtual DateTime Created { get; private set; }

        public virtual DateTime Updated { get; private set; }

        public void SetCreatedDate(DateTime dateTime)
        {
            if(dateTime == DateTime.MinValue) Created = dateTime;
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            Updated = dateTime;
        }
    }
}
