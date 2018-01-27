using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Exceptions;
using TimeManager.DAL.Connectivity;
using TimeManager.DAL.Querys;
using TimeManager.Entities;

namespace TimeManager.BL
{
    public class UsuarioBL
    {
        private static List<Usuario> GetData(DataTable QueryResult)
        {
            List<Usuario> ListUsuario = new List<Usuario>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListUsuario.Add(new Usuario()
                    {
                        Id = Row.IsNull("Id") ? 0 : int.Parse(Row["Id"].ToString()),
                        Nombre = Row.IsNull("Nombre") ? "" : Row["Nombre"].ToString(),
                        Apellido = Row.IsNull("Apellido") ? "" : Row["Apellido"].ToString(),
                        Correo = Row.IsNull("Correo") ? "" : Row["Correo"].ToString(),
                        Contrasenia = Row.IsNull("Contrasenia") ? "" : Row["Contrasenia"].ToString(),
                        EsActivo = Row.IsNull("EsActivo") ? false : bool.Parse(Row["EsActivo"].ToString()),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : DateTime.Parse(Row["FechaRegistro"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                // Exceptios
            }
            return ListUsuario;
        }

        public static List<Usuario> GetList()
        {
            List<Usuario> Usuario = new List<Usuario>();
            try
            {
                var Query = UsuarioQuery.GetList();
                var Result = Commands.ExecuteQuery(Query);

                Usuario = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Usuario;
        }

        public static Usuario GetById(int id)
        {
            Usuario Usuario = null;
            try
            {
                var Query = UsuarioQuery.GetById(id);
                var Result = Commands.ExecuteQuery(Query);

                Usuario = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Usuario;
        }

        public static Usuario Create(Usuario usuario)
        {
            try
            {
                var Query = UsuarioQuery.Create(usuario);
                var Result = int.Parse(Commands.ExecuteScalar(Query).ToString());

                usuario.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return usuario;
        }

        public static bool Update(Usuario usuario)
        {
            bool Result = false;
            try
            {
                var Query = UsuarioQuery.Update(usuario);
                Result = Commands.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Result;
        }

        public static bool Delete(int id)
        {
            bool Result = false;
            try
            {
                var Query = UsuarioQuery.Delete(id);
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
