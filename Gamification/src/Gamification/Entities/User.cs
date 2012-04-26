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
            Projects = new List<Project>();
        }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}