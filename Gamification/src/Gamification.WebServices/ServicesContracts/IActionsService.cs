using System.ServiceModel;
using Gamification.WebServices.DataContracts;
using Gamification.WebServices.DataContracts.Requests;
using Gamification.WebServices.DataContracts.Response;

namespace Gamification.WebServices.ServicesContracts
{
    [ServiceContract]
    [ServiceKnownType(typeof(ErrorTypes))]
    [ServiceKnownType(typeof(NumberOperation))]
    [ServiceKnownType(typeof(ChangeAction))]
    public interface IActionsService
    {
        [OperationContract]
        ActionResponse DoAction(ActionRequest actionRequest);

        [OperationContract]
        string TestPost();
    }
}
