using System;
using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Orders
{
    [DataContract]
    public class BasketDto
    {
        [DataMember]
        public Guid BasketGuid { get; set; }

        [DataMember]
        public string UserAssigned { get; set; }
    }
}
