using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;
using TimeManager.Common.Exceptions;

namespace TimeManager.DAL.Connectivity
{
    public class Connection : ExceptionBase
    {
        /// <summary>
        /// Establece y abre la conexión con la base de datos
        /// </summary>
        /// <returns></returns>
        public SqlConnection Connect()
        {
            SqlConnection Connection = null;
            try
            {
                Connection = new SqlConnection("data source=DESKTOP-LJ7LCS7;initial catalog=TimeManager;integrated security=True;MultipleActiveResultSets=True;");
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex, "Error al conectar con la base de datos", "public SqlConnection Connect()", "Connection", "TimeManager.DAL.Connectivity");
                HasError = true;
            }
            return Connection;
        }
    }
}
