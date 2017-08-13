using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Orders
{
    [DataContract]
    public enum OrderState
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Basket = 1,

        [EnumMember]
        Ordered = 2,

        [EnumMember]
        OrderInProgress = 3,

        [EnumMember]
        OrderSent = 4
    }
}
