using System;

namespace Gamification.Core.Entities
{
    [Serializable]
    public class Achievement : BaseEntity
    {
        public Project Project { get; set; }

        public string ImageName { get; set; }

        public string Name { get; set; }
    }
}