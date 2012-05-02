using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Core.ProjectSettings;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.Web.AutoMapper.Mappings.ViewModels
{
    public class GameActionViewModelConfiguration : IConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Project, GameActionViewModel>()
                .ForMember(x => x.CollectionBasedConstrains, opt => opt.MapFrom(x => x.CollectionConstraints))
                .ForMember(x => x.QtyBasedConstraints, opt => opt.MapFrom(x => x.NumericConstraints))
                .ForMember(x => x.Triggers, opt => opt.MapFrom(x => x.Triggers))
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Title, opt => opt.Ignore())
                ;

            Mapper.CreateMap<GameActionViewModel, GameAction>();
        }
    }
}