using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts
{
    [DataContract]
    public abstract class BaseResponseContract
    {
        [DataMember]
        public List<ErrorContract> Errors { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }
}
