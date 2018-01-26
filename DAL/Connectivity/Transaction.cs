using TimeManager.DAL.Connectivity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Exceptions;
using TimeManager.Common.Transaction;

namespace TimeManager.DAL.Connectivity
{
    public class Transaction : ExceptionBase
    {
        /// <summary>
        /// Lista de consultas a ejecutarse en la transacción
        /// </summary>
        public Query[] ListQuery { get; private set; }

        /// <summary>
        /// Determina si la transacción fue guardada
        /// </summary>
        public bool IsCommited { get; private set; }

        /// <summary>
        /// Determina si la transacción fue reversada
        /// </summary>
        public bool IsRollBacked { get; private set; }

        /// <summary>
        /// Determina si la transaccion fue exitosa
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Determina si la transacción fue interrumpida por una consulta con valor inesperado o error ocurrido
        /// </summary>
        public bool IsBroken { get; private set; }

        /// <summary>
        /// Determina cuál Id del query ocacionó la ruptura del código, por defecto es -1
        /// </summary>
        public int BrokenId { get; set; }

        /// <summary>
        /// Usuario del sistema
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Contador del identificador del query
        /// </summary>
        private int IdQuery = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ListQuery">Lista de consultas a ejecutar</param>
        /// <param name="User">Nombr del usuario que hace la transacción</param>
        public Transaction(string User, params Query[] ListQuery)
        {
            this.ListQuery = ListQuery ?? new Query[0];
            this.User = User;
        }

        /// <summary>
        /// Ejecuta la transacción actual
        /// </summary>
        public void Execute()
        {
            int BrokenId = -1;
            using (SqlConnection Con = new Connection().Connect())
            {
                using (SqlTransaction Tran = Con.BeginTransaction())
                {
                    try
                    {
                        foreach (var Query in ListQuery)
                        {
                            ExecuteQuery(Query, Tran);
                            if (IsBroken)
                            {
                                BrokenId = IdQuery;
                                break;
                            }
                        }

                        if (IsBroken)
                        {
                            Tran.Rollback();
                            IsRollBacked = true;
                            IsSuccess = false;
                        }
                        else
                        {
                            Tran.Commit();
                            IsSuccess = IsCommited = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Tran.Rollback(); IsBroken = IsRollBacked = true; IsSuccess = false; HasError = true;
                        ExceptionUtility.LogError(ex, "Error en la transacción", "void Execute()", "Transaction", "TimeManager.DAL");
                    }
                }
            }
        }

        /// <summary>
        /// Ejecuta recursivamente un query
        /// </summary>
        /// <param name="Query">Objeto query a ejecutar</param>
        private void ExecuteQuery(Query Query, SqlTransaction Tran)
        {
            if (Query != null)
            {
                Query.Result = new QueryResult();
                switch (Query.Type)
                {
                    case TypeCrud.Create:
                        Query.Result.ResultScalar = Commands.ExecuteScalar(Query.RawQuery, Query.Parameters.ToArray(), Tran);
                        Query.IsScalar = true;
                        IsBroken = long.Parse(Query.Result.ResultScalar.ToString()) < 1;
                        break;
                    case TypeCrud.Delete:
                        Query.Result.ResultNoQuery = Commands.ExecuteNonQuery(Query.RawQuery, Query.Parameters.ToArray(), Tran);
                        Query.IsNonQuery = true;
                        IsBroken = !Query.Result.ResultNoQuery;
                        break;
                    case TypeCrud.Update:
                        Query.Result.ResultNoQuery = Commands.ExecuteNonQuery(Query.RawQuery, Query.Parameters.ToArray(), Tran);
                        Query.IsNonQuery = true;
                        IsBroken = !Query.Result.ResultNoQuery;
                        break;
                    case TypeCrud.Query:
                        Query.Result.ResultQuery = Commands.ExecuteQuery(Query.RawQuery, Query.Parameters.ToArray(), Tran);
                        Query.IsQuery = true;
                        IsBroken = Query.Result.ResultQuery is null;
                        break;
                    default:
                        break;
                }
                Query.IsResolve = true;
                Query.IdQuery = IdQuery;
                if (IsBroken)
                {
                    Query.IsResolve = false;
                    return;
                }
                IdQuery++;
                if (Query.SubQuery != null)
                {
                    foreach (var SubQuery in Query.SubQuery)
                    {
                        ExecuteQuery(SubQuery, Tran);
                    }
                }
            }
        }
    }
}
