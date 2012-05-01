using System.Runtime.Serialization;
using Gamification.Core.Enums;

namespace Gamification.Services.DataContracts.Response
{
    [DataContract]
    public class CharacteristicChange
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AssignUnassign ChangeAction { get; set; }
    }
}