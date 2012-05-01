using System;
using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts
{
    [DataContract]
    public abstract class BaseSequrityContract
    {
        [DataMember]
        public Guid GamerKey { get; set; }
    }
}
