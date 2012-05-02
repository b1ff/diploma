using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response.BasicResponses
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