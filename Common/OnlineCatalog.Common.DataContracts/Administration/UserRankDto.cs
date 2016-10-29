using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Administration
{
    [DataContract(Namespace = "https://online.catalog.com/")]
    public enum UserRankDto
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        SystemAdministrator,

        [EnumMember]
        ShopAdministrator,

        [EnumMember]
        Client
    }
}
