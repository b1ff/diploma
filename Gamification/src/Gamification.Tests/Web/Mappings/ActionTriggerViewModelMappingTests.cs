using AutoMapper;
using Gamification.Core.Entities.Triggers;
using Gamification.Core.Enums;
using Gamification.Core.ProjectSettings;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Gamification.Web.Controllers;
using Gamification.Web.Utils.CommonViewModels;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Web.Mappings
{
    [TestFixture]
    public class ActionTriggerViewModelMappingTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AutoConfigurator.PerformAllConfiguration(typeof(HomeController).Assembly);
        }
        
        [Test]
        public void ToAddOrRemoveStatusTriggerTest()
        {
            const string title = "some title";
            var viewModel = new ActionTriggerViewModel();
            viewModel.Title = title;
            viewModel.AchievementAction = AssignUnassign.Assign;
            viewModel.Statuses = new DataSource();
            viewModel.StatusAction = AssignUnassign.Unassign;

            var trigger = Mapper.Map<ActionTriggerViewModel, AchievementsTrigger>(viewModel);

            trigger.Title.Should().Be.EqualTo(title);
            trigger.ActionWithAchievement.Should().Be.EqualTo(AssignUnassign.Assign);
        }
    }
}
