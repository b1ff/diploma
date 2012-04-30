using System.IO;
using Gamification.Core.Entities;

namespace Gamification.Core.StorageManagement
{
    public interface IStorage
    {
        string SaveAchievementImage(Stream stream, string fileName);

        string FullAchievementPath(Achievement achievement);
    }
}
