using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gamification.Core.Attributes;
using Gamification.Core.Enums;
using Gamification.Core.Extensions;
using Gamification.Core.Operations;

namespace Gamification.Core.Entities
{
    public class CommonBooleanConstraint : BaseEntity
    {
        public BooleanOperation BooleanOperation { get; set; }

        [Required]
        public string PropertyNameToCompare { get; set; }

        public double ValueToCompare { get; set; }
     
        public double GetPropertyValueToCompare(object entity)
        {
            var propertyInfo = entity.GetType().GetProperties()
                .FirstOrDefault(x => x.GetAttribute<PropertyToCompareAttribute>() != null 
                    && x.GetAttribute<PropertyToCompareAttribute>().Name == PropertyNameToCompare);

            if (propertyInfo == null)
                return 0;
            var value = (double?)propertyInfo.GetValue(entity, null);
            if (value.HasValue)
                return value.Value;

            return 0;
        }

        public bool GetResult(object comparableObject)
        {
            return this.GetResultProvider(comparableObject).GetResult();
        }

        public IBooleanResultProvider GetResultProvider(object comparableObject)
        {
            return new CommonBooleanResultsProvider(this, comparableObject);
        }
    }
}
