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
using TimeManager.DAL.Connectivity;
using TimeManager.Common.Query;

namespace TimeManager.DAL.Connectivity
{
    public class Commands : ExceptionBase
    {
        private static SqlConnection Conn = new Connection().Connect();

        /// <summary>
        /// Retorna una tabla de acuerdo a la consulta proporcionada
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="ListParameters"></param>
        /// <param name="Connection"></param>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(Query query)
        {
            DataTable DataTable = new DataTable();
            SqlTransaction Transaction = Conn.BeginTransaction();
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query.RawQuery, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(query.Parameters.ToArray());

                SqlDataReader sqldr = cmd.ExecuteReader();
                DataTable.Load(sqldr);

                Transaction.Commit();
                return DataTable;
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta en base de datos", "DataTable ExecuteQuery(string Query, SqlParameter[] ListParameters, SqlTransaction Transaction)", "Commands", "TimeManager.DAL.Connectivity");
                query.HasError = true;
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
        public static bool ExecuteNonQuery(Query query)
        {
            bool Result = false;
            SqlTransaction Transaction = Conn.BeginTransaction();
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query.RawQuery, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(query.Parameters.ToArray());                
                int Changes = cmd.ExecuteNonQuery();
                Result = Changes > 0;
                Transaction.Commit();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta escalar en base de datos", "bool ExecuteNonQuery(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)", "Commands", "TimeManager.DAL.Connectivity");
                query.HasError = true;
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
        public static object ExecuteScalar(Query query)
        {
            object Result = null;
            SqlTransaction Transaction = Conn.BeginTransaction();
            try
            {
                if (Transaction.Connection.State != ConnectionState.Open)
                {
                    Transaction.Connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query.RawQuery, Transaction.Connection, Transaction);
                cmd.Parameters.AddRange(query.Parameters.ToArray());

                Result = cmd.ExecuteScalar();
                Transaction.Commit();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                ExceptionUtility.LogError(ex, "Error al ejecutar consulta escalar en base de datos", "object ExecuteScalar(string NonQuery, SqlParameter[] ListParameters, SqlTransaction Transaction)",
                    "Commands", "TimeManager.DAL.Connectivity");
                query.HasError = true;
            }
            return Result;
        }
    }
}
