using System.ServiceModel;
using Common.DataModels;

namespace Common.Contracts
{
    [ServiceContract]
    public interface ISendError
    {
        [OperationContract]
        void SendError(ErrorModel error);
    }
}
