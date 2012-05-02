using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public class AchievementContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FileName { get; set; }
    }
}