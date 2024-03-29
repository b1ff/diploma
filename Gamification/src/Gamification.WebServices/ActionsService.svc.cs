﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using AutoMapper;
using Gamification.Core.DataAccess;
using Gamification.Core.Domain;
using Gamification.Core.Entities;
using Gamification.Core.Extensions;
using Gamification.Core.Specifications;
using Gamification.WebServices.DataContracts;
using Gamification.WebServices.DataContracts.Requests;
using Gamification.WebServices.DataContracts.Response;
using Gamification.WebServices.DataContracts.Response.BasicResponses;
using Gamification.WebServices.Helpers;
using Gamification.WebServices.ServicesContracts;

namespace Gamification.WebServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ActionsService : IActionsService
    {
        private readonly IRepository<Gamer> gamersRepository;
        private readonly IRepository<GameAction> actionsRepository;
        private readonly IRepository<Project> projectsRepository;

        public ActionsService(
            IRepository<Gamer> gamersRepository, 
            IRepository<GameAction> actionsRepository,
            IRepository<Project> projectsRepository)
        {
            this.gamersRepository = gamersRepository;
            this.actionsRepository = actionsRepository;
            this.projectsRepository = projectsRepository;
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ActionResponse DoAction(ActionRequest actionRequest)
        {
            var project = this.projectsRepository.FirstOrDefault(x => x.GamerKey == actionRequest.GamerKey, x => x.Levels);
            if (project == null)
            {
                return ResponseHelper.WrongProject();
            }

            var gamer = this.gamersRepository.FirstBySpec(new GamerByNameSpec(actionRequest.GamerName), 
                x => x.Project, x => x.Achievements, x => x.CurrentLevel, x => x.GamerStatuses);
            if (gamer == null)
            {
                gamer = EntityHelpers.CreateGamer(actionRequest.GamerName, project);
                this.gamersRepository.Add(gamer);
            }

            var action = this.actionsRepository.QueryIncluding(x => x.TriggersToCall, x => x.QtyBasedConstraints, x => x.StringCollectionConstraints)
                .FirstOrDefault(x => x.Project.Id == gamer.Project.Id && x.Id == actionRequest.ActionId);

            if (action == null)
            {
                return ResponseHelper.ResponseWithErrors(
                    new ErrorContract(ErrorTypes.Unexpected, "Action not found"));
            }

            var gamerSnapshot = gamer.Clone();
            IEnumerable<ActionPerformError> errors = action.PerformAction(gamer);
            if (errors.IsNotEmpty())
            {
                var errorContracts = Mapper.Map<IEnumerable<ErrorContract>>(errors);
                return ResponseHelper.ResponseWithErrors(errorContracts.ToArray());
            }

            this.actionsRepository.SaveChanges();
            return CreateResponse(gamerSnapshot, gamer);
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string TestPost()
        {
            return "Success";
        }

        private ActionResponse CreateResponse(Gamer oldGamer, Gamer newGamer)
        {
            var response = new ActionResponse();
            response.Success = true;
            response.AchievementChanges.Add(GetAchievementsDiff(oldGamer.Achievements, newGamer.Achievements, ChangeAction.Unassign));
            response.AchievementChanges.Add(GetAchievementsDiff(newGamer.Achievements, oldGamer.Achievements, ChangeAction.Assign));
            response.StatusChanges.Add(GetStatusDiff(oldGamer.GamerStatuses, newGamer.GamerStatuses, ChangeAction.Unassign));
            response.StatusChanges.Add(GetStatusDiff(newGamer.GamerStatuses, oldGamer.GamerStatuses, ChangeAction.Assign));

            if (oldGamer.CurrentLevel != newGamer.CurrentLevel)
            {
                var levelDiff = Math.Abs(oldGamer.CurrentLevel.LevelNumber - newGamer.CurrentLevel.LevelNumber);
                response.LevelChange = new NumericCharacteristicChange(levelDiff, oldGamer.CurrentLevel > newGamer.CurrentLevel ? NumberOperation.Decrease : NumberOperation.Increase);
            }

            if (oldGamer.Points != newGamer.Points)
            {
                var pointsDiff = Math.Abs(oldGamer.Points - newGamer.Points);
                response.PointsChange = new NumericCharacteristicChange(pointsDiff, oldGamer.Points > newGamer.Points ? NumberOperation.Decrease : NumberOperation.Increase);
            }

            return response;
        }

        private static IEnumerable<CharacteristicChange> GetAchievementsDiff(
            IEnumerable<Achievement> achievements1, 
            IEnumerable<Achievement> achievements2, 
            ChangeAction action)
        {
            if (achievements1 == null)
                return Enumerable.Empty<CharacteristicChange>();
            if (achievements2 == null)
                achievements2 = Enumerable.Empty<Achievement>();

            var achievementsDiff = achievements1.Where(x => !achievements2.Contains(x)).ToList();
            if (achievementsDiff.IsNotEmpty())
            {
                return achievementsDiff.Select(x => new CharacteristicChange(x.Name, action));
            }

            return Enumerable.Empty<CharacteristicChange>();
        }

        private static IEnumerable<CharacteristicChange> GetStatusDiff(
            IEnumerable<GamerStatus> statuses1,
            IEnumerable<GamerStatus> statuses2,
            ChangeAction action)
        {
            if (statuses1 == null)
                return Enumerable.Empty<CharacteristicChange>();
            if (statuses2 == null)
                statuses2 = Enumerable.Empty<GamerStatus>();

            var statusesDiff = statuses1.Where(x => !statuses2.Contains(x)).ToList();
            if (statusesDiff.IsNotEmpty())
            {
                return statusesDiff.Select(x => new CharacteristicChange(x.StatusName, action));
            }

            return Enumerable.Empty<CharacteristicChange>();
        }
    }
}
