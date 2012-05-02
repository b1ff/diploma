using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Requests
{
    [DataContract]
    public class ActionRequest : UserContract
    {
        [DataMember]
        public int ActionId { get; set; }
    }
}
