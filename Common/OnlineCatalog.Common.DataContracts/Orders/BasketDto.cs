using System;
using System.Runtime.Serialization;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Common.DataContracts.Orders
{
    [DataContract]
    public class BasketDto
    {
        public static readonly BasketDto EmptyBasket = new BasketDto {OrderState = OrderState.None};

        [DataMember]
        public Guid BasketGuid { get; set; }

        [DataMember]
        public Guid ShopGuid { get; set; }

        [DataMember]
        public ProductDto[] Products { get; set; }

        [DataMember]
        public OrderState OrderState { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string UserAssigned { get; set; }

        public override bool Equals(object obj)
        {
            BasketDto basket = obj as BasketDto;
            if (basket == null) return false;
            return basket.BasketGuid == this.BasketGuid;
        }
    }
}