using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;

namespace Gamification.Web.AutoMapper.Mappings.ViewModels
{
    public class ConstraintViewModelConfiguration : IAutomapperConfiguration
    {
        public void BuildMappings()
        {
            Mapper.CreateMap<Project, ConstraintViewModel>()
                .ForMember(x => x.Achievements, opt => opt.MapFrom(x => x.Achievements))
                .ForMember(x => x.Statuses, opt => opt.MapFrom(x => x.Statuses))
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Id))
                ;


            Mapper.CreateMap<ConstraintViewModel, BaseStringCollectionConstraint>()
                .Include<ConstraintViewModel, AchievementsConstraint>()
                .Include<ConstraintViewModel, StatusConstraint>()
                .ForMember(x => x.CollectionEqualityOperation, opt => opt.MapFrom(x => x.CollectionOperation))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                ;

            Mapper.CreateMap<ConstraintViewModel, AchievementsConstraint>();
            Mapper.CreateMap<ConstraintViewModel, StatusConstraint>();

            Mapper.CreateMap<ConstraintViewModel, BaseNumericBasedConstraint>()
                .Include<ConstraintViewModel, PointsBasedConstraint>()
                .Include<ConstraintViewModel, LevelBasedConstraint>()
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.BooleanOperation, opt => opt.MapFrom(x => x.BooleanOperation))
                ;

            Mapper.CreateMap<ConstraintViewModel, PointsBasedConstraint>()
                .ForMember(x => x.ValueToCompare, opt => opt.MapFrom(x => x.Points))
                ;

            Mapper.CreateMap<ConstraintViewModel, LevelBasedConstraint>()
                .ForMember(x => x.ValueToCompare, opt => opt.MapFrom(x => x.Level))
                ;
        }
    }
}