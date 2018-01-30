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
    public class ActividadQuerys
    {
        public static Query GetList(bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, BoletaId, Descripcion, EstadoId, FechaActividad, TiempoActividad, FechaRegistro, EsActivo FROM com.Actividad ",
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "WHERE EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query GetList(int boletaId, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, BoletaId, Descripcion, EstadoId, FechaActividad, TiempoActividad, FechaRegistro, EsActivo FROM com.Actividad " +
                           "WHERE BoletaId = @boletaId ",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter() { ParameterName = "boletaId", Value = boletaId }
                            },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query GetById(int id, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, BoletaId, Descripcion, EstadoId, FechaActividad, TiempoActividad, FechaRegistro, EsActivo FROM com.Actividad WHERE Id=@id ",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query Create(Actividad actividad)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = "INSERT INTO com.Actividad(Descripcion, BoletaId, EstadoId, FechaActividad, TiempoActividad, FechaRegistro, EsActivo) " +
                           "VALUES(@descripcion, @boletId, @estadoId, @fechaActividad, @tiempoActividad, @fechaRegistro, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("descripcion", actividad.Descripcion),
                                new SqlParameter("boletId", actividad.BoletaId),
                                new SqlParameter("estadoId", actividad.EstadoId),
                                new SqlParameter("fechaActividad", actividad.FechaActividad),
                                new SqlParameter("tiempoActividad", actividad.TiempoActividad),
                                new SqlParameter("fechaRegistro", actividad.FechaRegistro),
                                new SqlParameter("esActivo", actividad.EsActivo)
                            },
                Type = TypeCrud.Create
            };
            return QueryCreate;
        }

        public static Query Update(Actividad actividad)
        {
            Query QueryUpdate = new Query()
            {
                RawQuery = "UPDATE com.Actividad SET Descripcion=@descripcion, EstadoId=@estadoId, FechaActividad=@fechaActividad, TiempoActividad=@tiempoActividad, FechaRegistro=@fechaRegistro, EsActivo=@esActivo WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", actividad.Id),
                                new SqlParameter("descripcion", actividad.Descripcion),
                                new SqlParameter("estadoId", actividad.EstadoId),
                                new SqlParameter("fechaActividad", actividad.FechaActividad),
                                new SqlParameter("tiempoActividad", actividad.TiempoActividad),
                                new SqlParameter("fechaRegistro", actividad.FechaRegistro),
                                new SqlParameter("esActivo", actividad.EsActivo)
                                },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id, bool removePhysical)
        {
            string RawQuery = "UPDATE com.Actividad SET EsActivo = 0 WHERE Id = @id";

            if (removePhysical)
            {
                RawQuery = "DELETE com.Actividad WHERE Id = @id;";
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
