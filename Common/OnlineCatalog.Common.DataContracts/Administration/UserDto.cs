using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Administration
{
    [DataContract(Namespace = "http://onlinecatalog.com")]
    public class UserDto
    {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Surname { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsAdminMode { get; set; }

        [DataMember]
        public UserAddressDto Address { get; set; }

        [DataMember(IsRequired = true)]
        public string Login { get; set; }

        [DataMember(IsRequired = true)]
        public string Password { get; set; }
    }
}
