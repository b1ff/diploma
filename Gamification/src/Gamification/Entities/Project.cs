using System;
using System.Collections.Generic;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;

namespace Gamification.Core.Entities
{
    [Serializable]
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
            this.Statuses = new HashSet<GamerStatus>();
        }

        public Guid UserKey { get; set; }

        public Guid GamerKey { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public bool AcceptMultipleStatuses { get; set; }

        public ISet<Gamer> Gamers { get; set; }

        public ISet<Level> Levels { get; set; }

        public ISet<Achievement> Achievements { get; set; }
        
        [NonSerialized]
        private ISet<GameAction> gameActions;
        public ISet<GameAction> GameActions
        {
            get { return gameActions; }
            set { gameActions = value; }
        }

        public ISet<GamerStatus> Statuses { get; set; }

        [NonSerialized]
        private ISet<ActionTrigger> triggers;
        public ISet<ActionTrigger> Triggers
        {
            get { return triggers; }
            set { triggers = value; }
        }

        [NonSerialized]
        private ISet<BaseNumericBasedConstraint> numericConstraints;
        public ISet<BaseNumericBasedConstraint> NumericConstraints
        {
            get { return numericConstraints; }
            set { numericConstraints = value; }
        }

        [NonSerialized]
        private ISet<BaseStringCollectionConstraint> collectionConstraints;
        public ISet<BaseStringCollectionConstraint> CollectionConstraints
        {
            get { return collectionConstraints; }
            set { collectionConstraints = value; }
        }

        public bool UseLevels { get; set; }

        public bool UseAchievements { get; set; }

        public bool UsePoints { get; set; }
    }
}