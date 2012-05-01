using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts.Requests
{
    [DataContract]
    public class ActionRequest : BaseUserContract
    {
        [DataMember]
        public int ActionId { get; set; }
    }
}
