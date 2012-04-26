using System;
using System.Collections.Generic;
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
        }

        public Guid UserKey { get; set; }

        public Guid GamerKey { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public ISet<Gamer> Gamers { get; protected set; }

        public ISet<Level> Levels { get; protected set; }

        public ISet<Achievement> Achievements { get; protected set; }

        public ISet<GameAction> GameActions { get; protected set; }

        public ISet<ActionTrigger> Triggers { get; protected set; }
    }
}