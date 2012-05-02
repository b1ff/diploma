using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public class UserContract : BaseSequrityContract
    {
        [DataMember]
        public string GamerName { get; set; }
    }
}