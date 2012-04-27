using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamification.Core.Entities
{
    public class Level : BaseEntity
    {
        public Level()
        {
            UnlockedActions = new HashSet<GameAction>();
        }

        public int LevelNumber { get; set; }

        public int NeededPoints { get; set; }

        [Required]
        public Project Project { get; set; }

        public ISet<GameAction> UnlockedActions { get; protected set; }

        public static bool operator <(Level left, Level right)
        {
            return left.LevelNumber < right.LevelNumber;
        }

        public static bool operator >(Level left, Level right)
        {
            return left.LevelNumber > right.LevelNumber;
        }
    }
}
