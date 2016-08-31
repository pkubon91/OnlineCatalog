using System;
using Business.Products;

namespace Business.Orders
{
    public class Promotion : BaseDomainObject
    {
        public DateTime StartPromotion { get; set; }

        public DateTime EndPromotion { get; set; }

        public Product PromotionProduct { get; set; }

        public decimal NewBasePrice { get; set; }
    }
}
