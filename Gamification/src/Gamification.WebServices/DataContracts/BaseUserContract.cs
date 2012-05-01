using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts
{
    [DataContract]
    public abstract class BaseUserContract : BaseSequrityContract
    {
        [DataMember]
        public string GamerName { get; set; }
    }
}