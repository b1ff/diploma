using System;
using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public abstract class BaseSequrityContract
    {
        [DataMember]
        public Guid GamerKey { get; set; }
    }
}
