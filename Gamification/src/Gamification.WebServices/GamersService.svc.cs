using System;
using System.Linq;
using System.ServiceModel.Web;
using AutoMapper;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.WebServices.DataContracts;
using Gamification.WebServices.DataContracts.Response;
using Gamification.WebServices.Helpers;
using Gamification.WebServices.ServicesContracts;

namespace Gamification.WebServices
{
    public class GamersService : IGamersService
    {
        private readonly IRepository<Gamer> gamersRepository;
        private readonly IRepository<Project> projectsRepository;

        public GamersService(IRepository<Gamer> gamersRepository,
            IRepository<Project> projectsRepository)
        {
            this.gamersRepository = gamersRepository;
            this.projectsRepository = projectsRepository;
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GamerDataContract GamerData(string GamerName, Guid GamerKey)
        {
            var project = this.projectsRepository.FirstOrDefault(x => x.GamerKey == GamerKey, x => x.Levels);
            if (project == null)
                return new GamerDataContract().WithProjectError();

            var gamer = this.gamersRepository.FirstOrDefault(x => x.Project.Id == project.Id && x.UniqueKey == GamerName,
                x => x.Achievements, x => x.CurrentLevel, x => x.GamerStatuses);

            if (gamer == null)
            {
                gamer = EntityHelpers.CreateGamer(GamerName, project);
                if (gamer == null)
                    return new GamerDataContract().WithErrors(new ErrorContract(ErrorTypes.Unexpected, "Gamer name cannot be empty"));

                this.gamersRepository.Add(gamer);
            }

            this.gamersRepository.SaveChanges();
            var response = Mapper.Map<GamerDataContract>(gamer);
            var nextLevel = project.Levels.FirstOrDefault(x => x.LevelNumber == gamer.CurrentLevel.LevelNumber + 1);
            if (nextLevel == null)
            {
                response.PercentageToNextLevel = 100;
            }
            else
            {
                var pointsToNextLevel = nextLevel.NeededPoints - gamer.CurrentLevel.NeededPoints;
                var gamerPointsToNextLevel = gamer.Points - gamer.CurrentLevel.NeededPoints;
                response.PercentageToNextLevel = (int)(((double)gamerPointsToNextLevel / pointsToNextLevel) * 100);
            }

            response.Success = true;
            return response;
        }
    }
}
