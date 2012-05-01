using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts.Response
{
    [DataContract]
    public class ActionResponse : BaseResponseContract
    {
        [DataMember]
        public List<CharacteristicChange> AchievementChanges { get; set; }

        [DataMember]
        public NumericCharacteristicChange PointsChange { get; set; }

        [DataMember]
        public NumericCharacteristicChange LevelChange { get; set; }

        [DataMember]
        public List<CharacteristicChange> StatusChanges { get; set; }
    }
}
