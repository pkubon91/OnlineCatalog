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
    }
}
