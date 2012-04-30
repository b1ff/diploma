using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;

namespace Gamification.Web.AutoMapper.Mappings
{
    public class ProjectsNavMappingConfiguration : IAutomapperConfiguration
    {
        public void BuildMappings()
        {
            Mapper.CreateMap<Project, ProjectNavItemViewModel>()
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Title));
        }
    }
}