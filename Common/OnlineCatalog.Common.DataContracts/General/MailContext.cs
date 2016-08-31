using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.General
{
    [DataContract(Namespace = "http://online.catalog.com/")]
    public class MailContext
    {
        [DataMember]
        public string SenderAddress { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public MailContent EmailContent { get; set; }

        [DataMember]
        public string EmailTitle { get; set; }
    }
}
