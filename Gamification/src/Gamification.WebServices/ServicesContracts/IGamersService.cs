using System;
using System.ServiceModel;
using Gamification.WebServices.DataContracts.Response;

namespace Gamification.WebServices.ServicesContracts
{
    [ServiceContract]
    public interface IGamersService
    {
        [OperationContract]
        GamerDataContract GamerData(string GamerName, Guid GamerKey);
    }
}
