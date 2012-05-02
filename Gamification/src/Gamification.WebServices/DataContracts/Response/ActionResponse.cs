using System.Collections.Generic;
using System.Runtime.Serialization;
using Gamification.WebServices.DataContracts.Response.BasicResponses;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public class ActionResponse : BaseResponseContract
    {
        public ActionResponse()
        {
            AchievementChanges = new List<CharacteristicChange>();
            StatusChanges = new List<CharacteristicChange>();
        }

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
