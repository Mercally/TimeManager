using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Entities;
using TimeManager.Common.Query;
using TimeManager.Common.Methods;

namespace TimeManager.DAL.Querys
{
    public class BoletaQuerys
    {
        public static Query GetList(bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, TiempoInvertidoEn, ProyectoId, ClienteId, FechaRegistro, UsuarioId, DepartamentoId, EsActivo FROM com.Boleta ",
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
                RawQuery = "SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, TiempoInvertidoEn, ProyectoId, ClienteId, FechaRegistro, UsuarioId, DepartamentoId, EsActivo FROM com.Boleta Where Id = @id ",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }

        public static Query Create(Boleta boleta)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = "INSERT INTO com.Boleta(NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, TiempoInvertidoEn, ProyectoId, ClienteId, UsuarioId, DepartamentoId, FechaRegistro, EsActivo) " +
                                        "VALUES(@numeroBoleta, @fechaEntrada, @horaEntrada, @fechaSalida, @horaSalida, @tiempoEfectivo, @tiempoInvertidoEn, @proyectoId, @clienteId, @usuarioId, @departamentoId, @fechaRegistro, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("numeroBoleta", boleta.NumeroBoleta),
                                new SqlParameter("fechaEntrada", boleta.FechaEntrada),
                                new SqlParameter("horaEntrada", Formatting.TimeSpanToSqlTime(boleta.HoraEntrada)),
                                new SqlParameter("fechaSalida", boleta.FechaSalida),
                                new SqlParameter("horaSalida", Formatting.TimeSpanToSqlTime(boleta.HoraSalida)),
                                new SqlParameter("tiempoInvertidoEn", boleta.TiempoInvertidoEn),
                                new SqlParameter("tiempoEfectivo", Convert.ToDecimal(boleta.TiempoEfectivo)),
                                new SqlParameter("proyectoId", boleta.ProyectoId),
                                new SqlParameter("clienteId", boleta.ClienteId),
                                new SqlParameter("usuarioId", boleta.UsuarioId),
                                new SqlParameter("departamentoId", boleta.DepartamentoId),
                                new SqlParameter("fechaRegistro", boleta.FechaRegistro),
                                new SqlParameter("esActivo", boleta.EsActivo)
                            },
                Type = TypeCrud.Create
            };
            return QueryCreate;
        }

        public static Query Update(Boleta boleta)
        {
            Query QueryUpdate = new Query()
            {
                RawQuery = "UPDATE com.Boleta SET NumeroBoleta=@numeroBoleta, FechaEntrada=@fechaEntrada, HoraEntrada=@horaEntrada, " +
                            "FechaSalida=@fechaSalida, HoraSalida=@horaSalida, TiempoEfectivo=@tiempoEfectivo, TiempoInvertidoEn=@tiempoInvertidoEn, ProyectoId=@proyectoId, " +
                            "ClienteId=@clienteId, UsuarioId=@usuarioId, DepartamentoId=@departamentoId, FechaRegistro=@fechaRegistro, EsActivo=@esActivo WHERE Id=@id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", boleta.Id),
                                new SqlParameter("numeroBoleta", boleta.NumeroBoleta),
                                new SqlParameter("fechaEntrada", boleta.FechaEntrada),
                                new SqlParameter("horaEntrada", Formatting.TimeSpanToSqlTime(boleta.HoraEntrada)),
                                new SqlParameter("fechaSalida", boleta.FechaSalida),
                                new SqlParameter("horaSalida", Formatting.TimeSpanToSqlTime(boleta.HoraSalida)),
                                new SqlParameter("tiempoInvertidoEn", boleta.TiempoInvertidoEn),
                                new SqlParameter("tiempoEfectivo", Convert.ToDecimal(boleta.TiempoEfectivo)),
                                new SqlParameter("proyectoId", boleta.ProyectoId),
                                new SqlParameter("clienteId", boleta.ClienteId),
                                new SqlParameter("usuarioId", boleta.UsuarioId),
                                new SqlParameter("departamentoId", boleta.DepartamentoId),
                                new SqlParameter("fechaRegistro", boleta.FechaRegistro),
                                new SqlParameter("esActivo", boleta.EsActivo)
                            },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id, bool removePhysical = false)
        {
            string RawQuery = "UPDATE com.Boleta SET EsActivo = 0 WHERE Id = @id";

            if (removePhysical)
            {
                RawQuery = "DELETE com.Boleta WHERE Id = @id;";
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

        public static Query GetReporteFechas(string del, string hasta, bool onlyActives)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, TiempoInvertidoEn, ProyectoId, ClienteId, FechaRegistro, UsuarioId, DepartamentoId, EsActivo " +
                "FROM com.Boleta " +
                "WHERE (FechaEntrada >= @fechaEntrada AND FechaSalida <= @fechaSalida) ",
                Parameters = new List<SqlParameter>() {
                            new SqlParameter() { ParameterName = "fechaEntrada", Value = del },
                            new SqlParameter() { ParameterName = "fechaSalida", Value = hasta }
                },
                Type = TypeCrud.Query
            };

            if (onlyActives)
            {
                QueryGetById.RawQuery += "AND EsActivo=1;";
            }

            return QueryGetById;
        }
    }
}
