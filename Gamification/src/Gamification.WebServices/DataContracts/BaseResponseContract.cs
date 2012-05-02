using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public abstract class BaseResponseContract
    {
        protected BaseResponseContract()
        {
            Errors = new List<ErrorContract>();
        }

        [DataMember]
        public List<ErrorContract> Errors { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }
}
