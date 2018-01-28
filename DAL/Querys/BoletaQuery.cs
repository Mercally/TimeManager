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
    public class BoletaQuery
    {
        public static Query GetList()
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, ProyectoId, ClienteId, UsuarioId, DepartamentoId, EsActivo FROM com.Boleta;",
                //Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            return QueryGetById;
        }

        public static Query GetById(int id)
        {
            Query QueryGetById = new Query()
            {
                RawQuery = "SELECT Id, NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, ProyectoId, ClienteId, UsuarioId, DepartamentoId, EsActivo FROM com.Boleta Where Id = @id;",
                Parameters = new List<SqlParameter>() { new SqlParameter("id", id) },
                Type = TypeCrud.Query
            };

            return QueryGetById;
        }

        public static Query Create(Boleta boleta)
        {
            Query QueryCreate = new Query()
            {
                RawQuery = "INSERT INTO com.Boleta(NumeroBoleta, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, TiempoEfectivo, ProyectoId, ClienteId, UsuarioId, DepartamentoId, EsActivo) " +
                           "VALUES(@numeroBoleta, @fechaEntrada, @horaEntrada, @fechaSalida, @horaSalida, @tiempoEfectivo, @proyectoId, @clienteId, @usuarioId, @departamentoId, @esActivo); SELECT SCOPE_IDENTITY();",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("numeroBoleta", boleta.NumeroBoleta),
                                new SqlParameter("fechaEntrada", boleta.FechaEntera),
                                new SqlParameter("horaEntrada", boleta.HoraEntrada),
                                new SqlParameter("fechaSalida", boleta.FechaSalida),
                                new SqlParameter("horaSalida", boleta.HoraSalida),
                                new SqlParameter("proyectoId", boleta.ProyectoId),
                                new SqlParameter("clienteId", boleta.ClienteId),
                                new SqlParameter("usuarioId", boleta.UsuarioId),
                                new SqlParameter("departamentoId", boleta.DepartamentoId),
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
                RawQuery = "UPDATE com.Boleta SET NumeroBoleta = @numeroBoleta, FechaEntrada = @fechaEntrada, HoraEntrada = @horaEntrada, " +
                            "FechaSalida = @fechaSalida, HoraSalida = @horaSalida, TiempoEfectivo = @tiempoEfectivo, ProyectoId = @proyectoId, " +
                            "ClienteId = @clienteId, UsuarioId = @usuarioId, DepartamentoId = @departamentoId, EsActivo = @esActivo WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", boleta.Id),
                                new SqlParameter("numeroBoleta", boleta.NumeroBoleta),
                                new SqlParameter("fechaEntrada", boleta.FechaEntera),
                                new SqlParameter("horaEntrada", boleta.HoraEntrada),
                                new SqlParameter("fechaSalida", boleta.FechaSalida),
                                new SqlParameter("horaSalida", boleta.HoraSalida),
                                new SqlParameter("proyectoId", boleta.ProyectoId),
                                new SqlParameter("clienteId", boleta.ClienteId),
                                new SqlParameter("usuarioId", boleta.UsuarioId),
                                new SqlParameter("departamentoId", boleta.DepartamentoId),
                                new SqlParameter("esActivo", boleta.EsActivo)
                            },
                Type = TypeCrud.Update
            };
            return QueryUpdate;
        }

        public static Query Delete(int id)
        {
            Query QueryDelete = new Query()
            {
                RawQuery = "DELETE com.Boleta WHERE Id = @id;",
                Parameters = new List<SqlParameter>() {
                                new SqlParameter("id", id)
                                },
                Type = TypeCrud.Delete
            };
            return QueryDelete;
        }
    }
}
