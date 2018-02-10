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
    public class ActividadBL
    {
        private static List<Actividad> GetData(DataTable QueryResult)
        {
            List<Actividad> ListActividad = new List<Actividad>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListActividad.Add(new Actividad()
                    {
                        Id = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        Descripcion = Row.IsNull("Descripcion") ? "N/A" : Convert.ToString(Row["Descripcion"]),
                        EstadoId = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        BoletaId = Row.IsNull("BoletaId") ? 0 : Convert.ToInt32(Row["BoletaId"]),
                        FechaActividad = Row.IsNull("FechaActividad") ? DateTime.Now : Convert.ToDateTime(Row["FechaActividad"]),
                        TiempoActividad = Row.IsNull("TiempoActividad") ? 0 : Convert.ToDecimal(Row["TiempoActividad"]),
                        EsActivo = Row.IsNull("EsActivo") ? false : Convert.ToBoolean(Row["EsActivo"]),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : Convert.ToDateTime(Row["FechaRegistro"])
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListActividad;
        }

        public static List<Actividad> GetList(bool onlyActives = true)
        {
            List<Actividad> ListActividad = new List<Actividad>();
            try
            {
                var Query = ActividadQuerys.GetList(onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListActividad = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListActividad;
        }

        public static List<Actividad> GetList(int boletaId, bool onlyActives = true)
        {
            List<Actividad> ListActividad = new List<Actividad>();
            try
            {
                var Query = ActividadQuerys.GetList(boletaId, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListActividad = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListActividad;
        }

        public static Actividad GetById(int id, bool onlyActives = true)
        {
            Actividad Actividad = null;
            try
            {
                var Query = ActividadQuerys.GetById(id, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                Actividad = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Actividad;
        }

        public static Actividad Create(Actividad actividad)
        {
            try
            {
                var Query = ActividadQuerys.Create(actividad);
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

                actividad.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return actividad;
        }

        public static bool Update(Actividad actividad)
        {
            bool Result = false;
            try
            {
                var Query = ActividadQuerys.Update(actividad);
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
                var Query = ActividadQuerys.Delete(id, removePhysical);
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
