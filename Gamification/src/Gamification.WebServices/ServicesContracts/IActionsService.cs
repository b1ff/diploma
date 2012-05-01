using System.ServiceModel;
using Gamification.Services.DataContracts.Requests;
using Gamification.Services.DataContracts.Response;

namespace Gamification.WebServices.ServicesContracts
{
    [ServiceContract]
    public interface IActionsService
    {
          [OperationContract]
        ActionResponse DoAction(ActionRequest request);
    }
}
