using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public class ErrorContract
    {
        public ErrorContract()
        {
        }

        public ErrorContract(ErrorTypes errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        [DataMember]
        public ErrorTypes ErrorType { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
