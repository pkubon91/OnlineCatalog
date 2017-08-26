using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Common.DataContracts.Administration
{
    [DataContract(Namespace = "http://onlinecatalog.com")]
    public class UserDto
    {
        public static readonly UserDto EmptyUser = new UserDto() {IsAuthenticated = false};

        [DataMember]
        public Guid UserGuid { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Surname { get; set; }

        [DataMember(IsRequired = true)]
        public UserRankDto UserRank { get; set; }

        [DataMember]
        public UserAddressDto Address { get; set; }

        [DataMember(IsRequired = true)]
        public string Login { get; set; }

        [DataMember(IsRequired = true)]
        public string Password { get; set; }

        [DataMember]
        public IEnumerable<ShopDto> AssignedShops { get; set; } = new List<ShopDto>();

        [DataMember]
        public bool IsAuthenticated { get; private set; } = true;
    }
}
