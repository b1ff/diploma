using System.Linq;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Specifications;
using Gamification.Services.DataContracts;
using Gamification.Services.DataContracts.Requests;
using Gamification.Services.DataContracts.Response;
using Gamification.WebServices.ServicesContracts;

namespace Gamification.WebServices
{
    public class ActionService : IActionsService
    {
        private readonly IRepository<Gamer> gamersRepository;
        private readonly IRepository<GameAction> actionsRepository;

        public ActionService(
            IRepository<Gamer> gamersRepository, 
            IRepository<GameAction> actionsRepository)
        {
            this.gamersRepository = gamersRepository;
            this.actionsRepository = actionsRepository;
        }

        public ActionResponse DoAction(ActionRequest request)
        {
            var gamer = this.gamersRepository.FirstBySpec(new GamerByNameSpec(request.GamerName), x => x.Project);
            var action = this.actionsRepository.QueryIncluding(x => x.TriggersToCall, x => x.QtyBasedConstraints, x => x.StringCollectionConstraints)
                .FirstOrDefault(x => x.Project.Id == gamer.Project.Id && x.Id == request.ActionId);

            if (action == null)
                return ResponseWithErrors(new ErrorContract { ErrorType = ErrorTypes.Unexpected, Message = "Action not found" });

            return null;
        }

        private static ActionResponse ResponseWithErrors(params ErrorContract[] errorContracts)
        {
            var response = new ActionResponse();
            foreach (var errorContract in errorContracts)
            {
                response.Errors.Add(errorContract);
            }

            return response;
        }
    }
}
