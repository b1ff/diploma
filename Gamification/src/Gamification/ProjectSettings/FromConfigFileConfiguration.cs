using System;
using System.Configuration;

namespace Gamification.Core.ProjectSettings
{
    public class FromConfigFileConfiguration : IApplicationSettings
    {
        private readonly Lazy<string> connectionString = new Lazy<string>(() => ConfigurationManager.ConnectionStrings["Gamification.DB"].ConnectionString);

        public string ConnectionString
        {
            get { return connectionString.Value; }
        }
    }
}
