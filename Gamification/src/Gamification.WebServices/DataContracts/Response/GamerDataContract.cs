using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public class GamerDataContract : BaseResponseContract
    {
        [DataMember]
        public long Points { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public List<AchievementContract> Achievements { get; set; }

        [DataMember]
        public List<string> Statuses { get; set; }
    }
}