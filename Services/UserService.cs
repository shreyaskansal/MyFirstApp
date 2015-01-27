using Core.IContracts;
using Core.Models;
using log4net;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in both code and config file together.
    public class UserService : IUserService
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Function that authenticates the Login Action
        /// User AuthenticateUser(string username, string password)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User AuthenticateUser(string username, string password)
        {
            UserRepository ur = new UserRepository();
            return ur.AuthenticateUser(username, password);
        
        }
    }

}
