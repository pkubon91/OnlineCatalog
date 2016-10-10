using System;
using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts
{
    [DataContract(Namespace = "https://online.catalog.com/")]
    public class ServiceActionResult
    {
        public static readonly ServiceActionResult Successfull = new ServiceActionResult(ActionStatus.Successfull);

        public ActionStatus Status { get; private set; }
        public string Reason { get; private set; }
        public Exception ThrownException { get; private set; }

        private ServiceActionResult(ActionStatus status)
        {
            Status = status;
        }

        public ServiceActionResult(ActionStatus status, string reason) : this(status)
        {
            Reason = reason;
        }

        public ServiceActionResult(ActionStatus status, Exception thrownException) : this(status, "Unexpected error")
        {
            ThrownException = thrownException;
        }
    }
}
