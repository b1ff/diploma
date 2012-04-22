using System;
using Gamification.Core.Attributes;
using Gamification.Core.Entities;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Entities
{
    [TestFixture]
    public class CommonBooleanConstraintTests
    {
        private class FakeEntityWithoutProperty
        {
            [PropertyToCompare("Property")]
            public double Property { get; set; }

            [PropertyToCompare("NullableProperty")]
            public double? NullableProperty { get; set; }
        }

        [Test]
        public void GetPropertyValueToCompare_IfPassedObjectDoesntHaveNeededProperty_ShouldReturnZero()
        {
            var constraint = new CommonBooleanConstraint();
            constraint.PropertyNameToCompare = "NotExistingProp";
            var fake = new FakeEntityWithoutProperty();
            fake.Property = 10;

            var result = constraint.GetPropertyValueToCompare(fake);

            result.Should().Be.EqualTo(0);
        }

        [Test]
        public void GetPropertyValueToCompare_IfPassedObjectHaveNeededProperty_ShouldReturnPropertyValue()
        {
            var constraint = new CommonBooleanConstraint();
            constraint.PropertyNameToCompare = "Property";
            var fake = new FakeEntityWithoutProperty();
            fake.Property = 10;

            var result = constraint.GetPropertyValueToCompare(fake);

            result.Should().Be.EqualTo(10);
        }

        [Test]
        public void GetPropertyValueToCompare_IfPassedObjectHaveNullPropertyValue_ShouldReturnZero()
        {
            var constraint = new CommonBooleanConstraint();
            constraint.PropertyNameToCompare = "NullableProperty";
            var fake = new FakeEntityWithoutProperty();
            fake.NullableProperty = null;

            var result = constraint.GetPropertyValueToCompare(fake);

            result.Should().Be.EqualTo(0);
        }
    }
}
