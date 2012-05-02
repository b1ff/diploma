using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response
{
    [DataContract]
    public enum NumberOperation
    {
        [EnumMember]
        Increase,

        [EnumMember]
        Decrease
    }
}