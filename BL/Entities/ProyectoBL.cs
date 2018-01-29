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
    public class ProyectoBL
    {
        private static List<Proyecto> GetData(DataTable QueryResult)
        {
            List<Proyecto> ListProyecto = new List<Proyecto>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListProyecto.Add(new Proyecto()
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
            return ListProyecto;
        }

        public static List<Proyecto> GetList(bool onlyActives = true)
        {
            List<Proyecto> ListProyecto = new List<Proyecto>();
            try
            {
                var Query = ProyectoQuerys.GetList(onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListProyecto = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListProyecto;
        }

        public static Proyecto GetById(int id, bool onlyActives = true)
        {
            Proyecto Proyecto = null;
            try
            {
                var Query = ProyectoQuerys.GetById(id, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                Proyecto = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Proyecto;
        }

        public static Proyecto Create(Proyecto proyecto)
        {
            try
            {
                var Query = ProyectoQuerys.Create(proyecto);
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

                proyecto.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return proyecto;
        }

        public static bool Update(Proyecto proyecto)
        {
            bool Result = false;
            try
            {
                var Query = ProyectoQuerys.Update(proyecto);
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
                var Query = ProyectoQuerys.Delete(id, removePhysical);
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
