using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public enum ChangeAction
    {
        [EnumMember]
        Assign,

        [EnumMember]
        Unassign
    }
}