using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts
{
    [DataContract]
    public enum ErrorTypes
    {
        [EnumMember]
        Constraint,

        [EnumMember]
        WrongKey,

        [EnumMember]
        Unexpected
    }
}