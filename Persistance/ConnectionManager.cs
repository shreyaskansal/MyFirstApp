using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System.Web.Configuration;

namespace Persistence
{
    public class ConnectionManager
    {
        private static String connectionString = null;
        public ConnectionManager()
        {
            
        }

        /// <summary>
        /// This method returns a SqlConnection based on the connection strings entry in the web.config
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
            connectionString = conString.ConnectionString;
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// This method returns a stored procedure command 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcName"></param>
        /// <returns></returns>
        public static SqlCommand GetStoredProcedureCommand(SqlConnection connection, string commandName)
        {
            if (connection != null)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandName;
                command.CommandType = CommandType.StoredProcedure;
                return command;
            }

            return null;
        }

        public static SqlCommand PlainTextCommand(SqlConnection connection, string commandName)
        {
            if (connection != null)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandName;
                command.CommandType = CommandType.Text;
                return command;
            }

            return null;
        }

    }
}
