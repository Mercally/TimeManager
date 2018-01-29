using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Entities;
using TimeManager.Common.Query;

namespace TimeManager.DAL.Querys
{
    public class ClienteQuerys
    {
        public static Query GetList(bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, Nombre, FechaRegistro, EsActivo FROM neg.Cliente ",
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "WHERE EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query GetById(int id, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, Nombre, FechaRegistro, EsActivo FROM neg.Cliente WHERE Id = @id ",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query Create(Cliente cliente)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = "INSERT INTO neg.Cliente(Nombre, FechaRegistro, EsActivo) " +
                           "VALUES(@nombre, @fechaRegistro, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("nombre", cliente.Nombre),
                                new SqlParameter("fechaRegistro", cliente.FechaRegistro),
                                new SqlParameter("esActivo", cliente.EsActivo)
                            },
                Type = TypeCrud.Create
            };
            return QueryCreate;
        }

        public static Query Update(Cliente cliente)
        {
            Query QueryUpdate = new Query()
            {
                RawQuery = "UPDATE neg.Cliente SET Nombre=@nombre, FechaRegistro=@fechaRegistro, EsActivo=@esActivo WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", cliente.Id),
                                new SqlParameter("nombre", cliente.Nombre),
                                new SqlParameter("fechaRegistro", cliente.FechaRegistro),
                                new SqlParameter("esActivo", cliente.EsActivo)
                                },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id, bool removePhysical)
        {
            string RawQuery = "UPDATE neg.Cliente SET EsActivo = 0 WHERE Id = @id";

            if (removePhysical)
            {
                RawQuery = "DELETE neg.Cliente WHERE Id = @id;";
            }

            Query QueryDelete = new Query()
            {
                RawQuery = RawQuery,
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", id)
                                },
                Type = TypeCrud.Delete
            };
            return QueryDelete;
        }
    }
}
