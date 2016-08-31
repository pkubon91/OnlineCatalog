using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.General
{
    [DataContract(Namespace = "http://online.catalog.com/")]
    public enum MailMessageType
    {
        [EnumMember]
        Registration,
        [EnumMember]
        Empty
    }
}
