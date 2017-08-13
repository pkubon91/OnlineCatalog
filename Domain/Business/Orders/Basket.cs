using System;
using System.Collections.Generic;
using Business.Administration;
using Business.Groups;
using Business.Products;

namespace Business.Orders
{
    public class Basket : BaseDomainObject, IAuditable
    {
        public Basket()
        {
            BasketProducts = new List<Product>();
        }

        public virtual BasketState State { get; set; }

        public virtual bool IsRealized { get; set; }

        public virtual User Owner { get; set; }

        public virtual IList<Product> BasketProducts { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

        public Shop BasketShop { get; set; }

        public void SetCreatedDate(DateTime dateTime)
        {
            Created = dateTime;
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            Updated = dateTime;
        }

        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            BasketProducts.Add(product);
        }
    }
}
