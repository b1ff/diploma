using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Gamification.Core.Entities;
using Gamification.Core.StorageManagement;

namespace Gamification.Web.Utils.WebStorage
{
    public class Storage : IStorage
    {
        private readonly UrlHelper urlHelper;
        private const string achievementVirualPath = "~/Storage/Achievements/";
        private const string achievementPath = "Storage\\Achievements";

        public Storage(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public string SaveAchievementImage(Stream stream, string fileName)
        {
            var imageName = Guid.NewGuid() + Path.GetExtension(fileName);
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, achievementPath, imageName);
            var image = Image.FromStream(stream);
            image.Save(fullPath);

            return imageName;
        }

        public string FullAchievementPath(Achievement achievement)
        {
            return this.urlHelper.Content(achievementVirualPath + achievement.ImageName);
        }
    }
}