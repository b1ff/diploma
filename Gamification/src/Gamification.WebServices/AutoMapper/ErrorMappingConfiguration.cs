using AutoMapper;
using Gamification.Core.Domain;
using Gamification.WebServices.DataContracts;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.WebServices.AutoMapper
{
    public class ErrorMappingConfiguration : IConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<ActionPerformError, ErrorContract>()
                .ForMember(x => x.Message, opt => opt.MapFrom(x => x.Message))
                .ForMember(x => x.ErrorType, opt => opt.MapFrom(x => ErrorTypes.Constraint))
                ;
        }
    }
}