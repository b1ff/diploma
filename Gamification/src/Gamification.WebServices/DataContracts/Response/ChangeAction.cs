using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public enum ChangeAction
    {
        [EnumMember(Value = "Assign")]
        Assign,

        [EnumMember(Value = "Unassign")]
        Unassign
    }
}