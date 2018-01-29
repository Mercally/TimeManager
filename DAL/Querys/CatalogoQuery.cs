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
    public class CatalogoQuerys
    {
        public static Query GetList(string tableName, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = $"SELECT Id, Nombre, FechaRegistro, EsActivo FROM cat.{tableName} ",
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "WHERE EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query GetById(int id, string tableName, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = $"SELECT Id, Nombre, FechaRegistro, EsActivo FROM cat.{tableName} WHERE Id = @id ",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query Create(Catalogo catalogo, string tableName)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = $"INSERT INTO cat.{tableName}(Nombre, FechaRegistro, EsActivo) " +
                           "VALUES(@nombre, @fechaRegistro, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("nombre", catalogo.Nombre),
                                new SqlParameter("fechaRegistro", catalogo.FechaRegistro),
                                new SqlParameter("esActivo", catalogo.EsActivo)
                            },
                Type = TypeCrud.Create
            };
            return QueryCreate;
        }

        public static Query Update(Catalogo catalogo, string tableName)
        {
            Query QueryUpdate = new Query()
            {
                RawQuery = $"UPDATE cat.{tableName} SET Nombre=@nombre, FechaRegistro=@fechaRegistro, EsActivo=@esActivo WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", catalogo.Id),
                                new SqlParameter("nombre", catalogo.Nombre),
                                new SqlParameter("fechaRegistro", catalogo.FechaRegistro),
                                new SqlParameter("esActivo", catalogo.EsActivo)
                                },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id, string tableName, bool removePhysical)
        {
            string RawQuery = $"UPDATE cat.{tableName} SET EsActivo = 0 WHERE Id = @id";

            if (removePhysical)
            {
                RawQuery = $"DELETE cat.{tableName} WHERE Id = @id;";
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
