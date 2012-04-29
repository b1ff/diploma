using System;
using System.Collections.Generic;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;

namespace Gamification.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            this.Gamers = new HashSet<Gamer>();
            this.Levels = new HashSet<Level>();
            this.Achievements = new HashSet<Achievement>();
            this.GameActions = new HashSet<GameAction>();
            this.Triggers = new HashSet<ActionTrigger>();
            this.NumericConstraints = new HashSet<BaseNumericBasedConstraint>();
            this.CollectionConstraints = new HashSet<BaseStringCollectionConstraint>();
        }

        public Guid UserKey { get; set; }

        public Guid GamerKey { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public ISet<Gamer> Gamers { get; set; }

        public ISet<Level> Levels { get; set; }

        public ISet<Achievement> Achievements { get; set; }

        public ISet<GameAction> GameActions { get; set; }

        public ISet<ActionTrigger> Triggers { get; set; }

        public ISet<BaseNumericBasedConstraint> NumericConstraints { get; set; }

        public ISet<BaseStringCollectionConstraint> CollectionConstraints { get; set; }

        public bool UseLevels { get; set; }

        public bool UseAchievements { get; set; }

        public bool UsePoints { get; set; }
    }
}