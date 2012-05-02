using System;
using System.Linq;
using System.Reflection;

namespace Gamification.Core.ProjectSettings
{
    public static class AutoConfigurator
    {
        public static void PerformAllConfiguration(Assembly assembly)
        {
            // todo: maybe use ioc for this purposes
            var mappingConfigTypes = assembly.GetTypes()
                .Where(x => (typeof(IConfiguration).IsAssignableFrom(x) && !x.IsAbstract)).AsParallel();

            foreach (var configType in mappingConfigTypes)
            {
                var config = (IConfiguration)Activator.CreateInstance(configType);
                config.Configure();
            }
        }
    }
}
