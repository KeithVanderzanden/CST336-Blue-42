using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AESHiringManagement.Content.Services
{
    [ServiceContract]
    public interface IWaitList
    {
        [OperationContract]
        void AddApplicant(string name, int id);
    }
}
