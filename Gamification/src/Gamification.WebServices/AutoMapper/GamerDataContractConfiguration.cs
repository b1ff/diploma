using System.Linq;
using AutoMapper;
using Gamification.Core.Entities;
using Gamification.WebServices.DataContracts.Response;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.WebServices.AutoMapper
{
    public class GamerDataContractConfiguration : IConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Gamer, GamerDataContract>()
                .ForMember(x => x.Level, opt => opt.MapFrom(x => x.CurrentLevel.LevelNumber))
                .ForMember(x => x.Points, opt => opt.MapFrom(x => x.Points))
                .ForMember(x => x.Statuses, opt => opt.MapFrom(x => x.GamerStatuses.Select(s => s.StatusName)))
                .ForMember(x => x.Achievements, opt => opt.MapFrom(x => x.Achievements))
                ;


            Mapper.CreateMap<Achievement, AchievementContract>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => x.ImageName))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                ;
        }
    }
}