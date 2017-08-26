using System;
using System.Runtime.Serialization;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Common.DataContracts.Groups
{
    [DataContract(Namespace = "https://online.catalog.com/")]
    public class ShopDto
    {
        public static ShopDto EmptyShop = new ShopDto();

        [DataMember]
        public Guid ShopGuid { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public UserAddressDto Address { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            ShopDto shop = obj as ShopDto;
            if (shop == null) return false;
            return shop.ShopGuid == ShopGuid;
        }
    }
}
