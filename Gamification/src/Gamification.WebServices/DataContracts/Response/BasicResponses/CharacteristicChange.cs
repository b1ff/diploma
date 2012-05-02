using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response.BasicResponses
{
    [DataContract]
    public class CharacteristicChange
    {
        public CharacteristicChange()
        {
        }

        public CharacteristicChange(string name, ChangeAction changeAction)
        {
            Name = name;
            ChangeAction = changeAction;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ChangeAction ChangeAction { get; set; }
    }
}