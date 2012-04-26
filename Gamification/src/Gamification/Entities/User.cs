using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    /// <summary>
    /// NOTE: User is always admin of project.
    /// </summary>
    public class User : BaseEntity
    {
        public User()
        {
            Projects = new HashSet<Project>();
        }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public ISet<Project> Projects { get; protected set; }
    }
}