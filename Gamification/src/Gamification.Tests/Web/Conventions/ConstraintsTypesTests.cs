using System.Linq;
using Gamification.Core.Entities.Constraints;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Web.Conventions
{
    [TestFixture]
    public class ConstraintsTypesTests
    {
        [Test]
        public void ConstraintsTypeEnum_ShouldContainsValuesEqualToConstraintsClasses()
        {
            var enumValues = typeof(ConstraintTypes).GetEnumValues();
            var constraintsQuantity =
                typeof(BaseConstraint<>).Assembly.GetTypes().Where(
                    x => (typeof(BaseStringCollectionConstraint).IsAssignableFrom(x) || typeof(BaseNumericBasedConstraint).IsAssignableFrom(x)) && !x.IsAbstract).ToList();


            enumValues.Length.Should().Be.EqualTo(constraintsQuantity.Count);
        }
    }
}
