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
    public class UsuarioQuery
    {
        public static Query GetList()
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, Nombre, Apellido, Correo, Contrasenia, FechaRegistro, EsActivo FROM seg.Usuario;",
                //Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            return QueryGetById;
        }

        public static Query GetById(int id)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, Nombre, Apellido, Correo, Contrasenia, FechaRegistro, EsActivo FROM seg.Usuario WHERE Id = @id;",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            return QueryGetById;
        }

        public static Query Create(Usuario usuario)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = "INSERT INTO seg.Usuario(Nombre, Apellido, Correo, Contrasenia, FechaRegistro, EsActivo) " +
                           "VALUES(@nombre, @apellido, @correo, @contrasenia, @fechaRegistro, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("nombre", usuario.Nombre),
                                new SqlParameter("apellido", usuario.Apellido),
                                new SqlParameter("correo", usuario.Correo),
                                new SqlParameter("contrasenia", usuario.Contrasenia),
                                new SqlParameter("fechaRegistro", usuario.FechaRegistro),
                                new SqlParameter("esActivo", usuario.EsActivo)
                            },
                Type = TypeCrud.Create
            };
            return QueryCreate;
        }

        public static Query Update(Usuario usuario)
        {
            Query QueryUpdate = new Query()
            {
                RawQuery = "UPDATE seg.Usuario SET Nombre=@nombre, Apellido=@apellido, Correo=@correo, Contrasenia=@contrasenia, FechaRegistro=@fechaRegistro, EsActivo=@esActivo WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", usuario.Id),
                                new SqlParameter("nombre", usuario.Nombre),
                                new SqlParameter("apellido", usuario.Apellido),
                                new SqlParameter("correo", usuario.Correo),
                                new SqlParameter("contrasenia", usuario.Contrasenia),
                                new SqlParameter("fechaRegistro", usuario.FechaRegistro),
                                new SqlParameter("esActivo", usuario.EsActivo)
                                },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id)
        {
            Query QueryDelete = new Query()
            {
                RawQuery = "DELETE seg.Usuario WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", id)
                                },
                Type = TypeCrud.Delete
            };
            return QueryDelete;
        }
    }
}
