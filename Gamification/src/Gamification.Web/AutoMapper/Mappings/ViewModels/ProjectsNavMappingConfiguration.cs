using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Core.ProjectSettings;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.Web.AutoMapper.Mappings.ViewModels
{
    public class ProjectsNavMappingConfiguration : IConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Project, ProjectNavItemViewModel>()
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Title));
        }
    }
}