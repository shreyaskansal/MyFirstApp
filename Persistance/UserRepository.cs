using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using log4net;

namespace Persistance
{
    public class UserRepository
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private System.Data.SqlClient.SqlConnection conn;

        /// <summary>
        /// Establishes Connection
        /// </summary>
        public UserRepository()
        {
            conn = ConnectionManager.GetConnection();
            conn.Open();
        }

        /// <summary>
        /// Function that authenticates the Login Action
        /// User AuthenticateUser(string username, string password)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User AuthenticateUser(string username, string password)
        {
            User user = null;
            SqlCommand sqlcmd = ConnectionManager.PlainTextCommand(conn,string.Format("SELECT * FROM EMPLOYEE WHERE Email='{0}' AND Password='{1}'", username, password));
            SqlDataReader reader = sqlcmd.ExecuteReader();

            if (reader.Read())
            {
                user = new User();
                user.Username = Convert.ToString(reader["Email"]);
                user.Password = Convert.ToString(reader["Password"]);
            }
            return user;
        }

    }
}
