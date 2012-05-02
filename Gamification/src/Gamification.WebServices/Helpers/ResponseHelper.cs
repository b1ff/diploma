using Gamification.Core.Extensions;
using Gamification.WebServices.DataContracts;
using Gamification.WebServices.DataContracts.Response;

namespace Gamification.WebServices.Helpers
{
    public static class ResponseHelper
    {
        public static ActionResponse ResponseWithErrors(params ErrorContract[] errorContracts)
        {
            return new ActionResponse().WithErrors(errorContracts);
        }

        public static TResponse WithErrors<TResponse>(this TResponse response, params ErrorContract[] errorContracts)
            where TResponse : BaseResponseContract
        {
            response.Errors.Add(errorContracts);
            return response;
        }

        public static ActionResponse WrongProject()
        {
            return new ActionResponse().WithProjectError();
        }

        public static TResponse WithProjectError<TResponse>(this TResponse response)
            where TResponse : BaseResponseContract, new()
        {
            return response.WithErrors(new ErrorContract(ErrorTypes.WrongKey, "Project not found"));
        }
    }
}