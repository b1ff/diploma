using System.Collections.Generic;
using Gamification.Core.Entities;

namespace Gamification.Core.DataAccess
{
    public interface IAchievementsRepository : IRepository<Achievement>
    {
        IEnumerable<Achievement> GetAchievements();
    }
}