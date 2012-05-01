using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts
{
    [DataContract]
    public class ErrorContract
    {
        [DataMember]
        public ErrorTypes ErrorType { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
