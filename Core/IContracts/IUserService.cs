using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{   
   [ServiceContract]
    public interface IUserService
    {
       [OperationContract]
       User AuthenticateUser(string username, string password);
    }

}
