namespace Gamification.Core.Entities
{
    public class Achievement : BaseEntity
    {
        public Project Project { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }
    }
}