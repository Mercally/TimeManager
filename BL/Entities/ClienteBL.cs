using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Entities;
using TimeManager.Common.Exceptions;
using TimeManager.DAL.Connectivity;
using TimeManager.DAL.Querys;

namespace TimeManager.BL.Entities
{
    public class ClienteBL
    {
        private static List<Cliente> GetData(DataTable QueryResult)
        {
            List<Cliente> ListCliente = new List<Cliente>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListCliente.Add(new Cliente()
                    {
                        Id = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        Nombre = Row.IsNull("Nombre") ? "" : Convert.ToString(Row["Nombre"]),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : Convert.ToDateTime(Row["FechaRegistro"]),
                        EsActivo = Row.IsNull("EsActivo") ? false : Convert.ToBoolean(Row["EsActivo"])
                    });
                }
            }
            catch (Exception ex)
            {
                // Exceptios
            }
            return ListCliente;
        }

        public static List<Cliente> GetList(bool onlyActives = true)
        {
            List<Cliente> ListCliente = new List<Cliente>();
            try
            {
                var Query = ClienteQuerys.GetList(onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListCliente = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListCliente;
        }

        public static Cliente GetById(int id, bool onlyActives = true)
        {
            Cliente Cliente = null;
            try
            {
                var Query = ClienteQuerys.GetById(id, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                Cliente = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Cliente;
        }

        public static Cliente Create(Cliente cliente)
        {
            try
            {
                var Query = ClienteQuerys.Create(cliente);
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

                cliente.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return cliente;
        }

        public static bool Update(Cliente cliente)
        {
            bool Result = false;
            try
            {
                var Query = ClienteQuerys.Update(cliente);
                Result = Commands.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Result;
        }

        public static bool Delete(int id, bool removePhysical = false)
        {
            bool Result = false;
            try
            {
                var Query = ClienteQuerys.Delete(id, removePhysical);
                Result = Commands.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Result;
        }
    }
}
