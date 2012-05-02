using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public abstract class BaseUserContract : BaseSequrityContract
    {
        [DataMember]
        public string GamerName { get; set; }
    }
}