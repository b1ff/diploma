using System.Runtime.Serialization;

namespace Gamification.WebServices.DataContracts.Response.BasicResponses
{
    [DataContract]
    public class NumericCharacteristicChange
    {
        public NumericCharacteristicChange()
        {
        }

        public NumericCharacteristicChange(double changeQuantity, NumberOperation changeOperation)
        {
            ChangeQuantity = changeQuantity;
            ChangeOperation = changeOperation;
        }

        [DataMember]
        public double ChangeQuantity { get; set; }

        [DataMember]
        public NumberOperation ChangeOperation { get; set; }
    }
}