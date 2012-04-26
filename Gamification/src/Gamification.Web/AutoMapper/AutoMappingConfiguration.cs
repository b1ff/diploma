using System;
using System.Linq;

namespace Gamification.Web.AutoMapper
{
    public static class AutoMappingConfiguration
    {
        public static void ConfigureAutoMapper()
        {
            var mappingConfigTypes = typeof(IAutomapperConfiguration).Assembly.GetTypes()
                .Where(x => (typeof(IAutomapperConfiguration).IsAssignableFrom(x) && !x.IsAbstract)).AsParallel();

            foreach(var configType in mappingConfigTypes)
            {
                var config = (IAutomapperConfiguration)Activator.CreateInstance(configType);
                config.BuildMappings();
            }
        }
    }
}