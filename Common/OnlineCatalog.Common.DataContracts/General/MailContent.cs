using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.General
{
    [DataContract(Namespace = "http://online.catalog.com/")]
    public class MailContent
    {
        internal MailContent()
        {
            
        }

        public MailContent(MailMessageType messageType)
        {
            MessageType = messageType;
        }

        [DataMember]
        public MailMessageType MessageType { get; private set; }

        [DataMember]
        public string MessageBody { get; set; }

        [DataMember]
        public string ReceiverName { get; set; }
    }
}
