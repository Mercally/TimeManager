using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;
using System.Data;
using TimeManager.Common.Exceptions;

namespace TimeManager.DAL.Connectivity
{
    public class Commands : ExceptionBase
    {
        /// <summary>
        /// Retorna una tabla de acuerdo a la consulta proporcionada
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="ListParameters"></param>
        /// <param name="Connection"></param>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string Query, SqlParameter[] ListParameters, SqlTransaction Transaction)
        {
            DataTable DataTable = new DataTable();
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(Query, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(ListParameters);

                SqlDataReader sqldr = cmd.ExecuteReader();
                DataTable.Load(sqldr);

                return DataTable;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta en base de datos", "DataTable ExecuteQuery(string Query, SqlParameter[] ListParameters, SqlTransaction Transaction)", "Commands", "TimeManager.DAL.Connectivity");
            }
            return DataTable;
        }

        /// <summary>
        /// Ejecuta comandos delete y update
        /// </summary>
        /// <param name="NonQuery"></param>
        /// <param name="Connection"></param>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)
        {
            bool Result = false;
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(NonQuery, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(ListParameters);                
                int Changes = cmd.ExecuteNonQuery();
                Result = Changes > 0;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta escalar en base de datos", "bool ExecuteNonQuery(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)", "Commands", "TimeManager.DAL.Connectivity");
            }
            return Result;
        }

        /// <summary>
        /// Ejecuta un comando que devuelve un solo valor
        /// </summary>
        /// <param name="NonQuery"></param>
        /// <param name="Connection"></param>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)
        {
            object Result = null;
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(NonQuery, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(ListParameters);

                Result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta escalar en base de datos", "object ExecuteScalar(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)",
                    "Commands", "TimeManager.DAL.Connectivity");
            }
            return Result;
        }
    }
}
