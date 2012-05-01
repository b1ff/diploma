using System.Runtime.Serialization;

namespace Gamification.Services.DataContracts.Response
{
    [DataContract]
    public class NumericCharacteristicChange
    {
        [DataMember]
        public double ChangeQuantity { get; set; }

        [DataMember]
        public NumberOperation ChangeOperation { get; set; }
    }
}