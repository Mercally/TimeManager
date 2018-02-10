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
                        Id = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        Nombre = Row.IsNull("Nombre") ? "" : Convert.ToString(Row["Nombre"]),
                        Apellido = Row.IsNull("Apellido") ? "" : Convert.ToString(Row["Apellido"]),
                        Correo = Row.IsNull("Correo") ? "" : Convert.ToString(Row["Correo"]),
                        Contrasenia = Row.IsNull("Contrasenia") ? "" : Convert.ToString(Row["Contrasenia"]),
                        EsActivo = Row.IsNull("EsActivo") ? false : Convert.ToBoolean(Row["EsActivo"]),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : Convert.ToDateTime(Row["FechaRegistro"])
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListUsuario;
        }

        public static List<Usuario> GetList(bool onlyActives = true)
        {
            List<Usuario> ListUsuario = new List<Usuario>();
            try
            {
                var Query = UsuarioQuerys.GetList(onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListUsuario = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListUsuario;
        }

        public static Usuario GetById(int id, bool onlyActives = true)
        {
            Usuario Usuario = null;
            try
            {
                var Query = UsuarioQuerys.GetById(id, onlyActives);
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
                var Query = UsuarioQuerys.Create(usuario);
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

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
                var Query = UsuarioQuerys.Update(usuario);
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
                var Query = UsuarioQuerys.Delete(id, removePhysical);
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
