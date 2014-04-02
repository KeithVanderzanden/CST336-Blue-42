using AESHiringManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AESHiringManagement.Content.Services
{
     public class WaitList : IWaitList
    {
        public void AddApplicant(string name, int id)
        {
            Dashboard.addApplicant(name, id);
        }
    }
}
