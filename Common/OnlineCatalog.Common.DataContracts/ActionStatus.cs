using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCatalog.Common.DataContracts
{
    [DataContract(Namespace = "https://online.catalog.com")]
    public enum ActionStatus
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Successfull,

        [EnumMember]
        NotSuccessfull,

        [EnumMember]
        WithException
    }
}
