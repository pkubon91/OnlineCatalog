using System;
using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Administration
{
    [DataContract(Namespace = "https://onlinecatalog.com")]
    public class UserAddressDto
    {
        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string BuildingNumber { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public Guid AddressGuid { get; set; }
    }
}
