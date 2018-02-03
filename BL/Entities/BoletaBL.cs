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
using TimeManager.Common.Methods;

namespace TimeManager.BL.Entities
{
    public class BoletaBL
    {
        private static List<Boleta> GetData(DataTable QueryResult)
        {
            List<Boleta> ListBoleta = new List<Boleta>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListBoleta.Add(new Boleta()
                    {
                        Id = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        ClienteId = Row.IsNull("ClienteId") ? 0 : Convert.ToInt32(Row["ClienteId"]),
                        DepartamentoId = Row.IsNull("DepartamentoId") ? 0 : Convert.ToInt32(Row["DepartamentoId"]),
                        FechaEntrada = Row.IsNull("FechaEntrada") ? DateTime.Now : Convert.ToDateTime(Row["FechaEntrada"]),
                        FechaSalida = Row.IsNull("FechaSalida") ? DateTime.Now : Convert.ToDateTime(Row["FechaSalida"]),
                        NumeroBoleta = Row.IsNull("NumeroBoleta") ? "N/A" : Convert.ToString(Row["NumeroBoleta"]),
                        ProyectoId = Row.IsNull("ProyectoId") ? 0 : Convert.ToInt32(Row["ProyectoId"]),
                        TiempoEfectivo = Row.IsNull("TiempoEfectivo") ? 0 : Convert.ToDecimal(Row["TiempoEfectivo"]),
                        TiempoInvertidoEn = Row.IsNull("TiempoInvertidoEn") ? 0 : Convert.ToInt32(Row["TiempoInvertidoEn"]),
                        UsuarioId = Row.IsNull("UsuarioId") ? 0 : Convert.ToInt32(Row["UsuarioId"]),
                        EsActivo = Row.IsNull("EsActivo") ? false : Convert.ToBoolean(Row["EsActivo"]),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : Convert.ToDateTime(Row["FechaRegistro"])
                    });
                }
            }
            catch (Exception ex)
            {
                // Exceptios
            }
            return ListBoleta;
        }

        public static List<Boleta> GetList(bool onlyActives = true)
        {
            List<Boleta> ListBoleta = new List<Boleta>();
            try
            {
                var Query = BoletaQuerys.GetList(onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListBoleta = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListBoleta;
        }

        public static Boleta GetById(int id, bool onlyActives = true)
        {
            Boleta Boleta = null;
            try
            {
                var Query = BoletaQuerys.GetById(id, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                Boleta = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Boleta;
        }

        public static Boleta Create(Boleta boleta)
        {
            try
            {
                var Query = BoletaQuerys.Create(boleta);
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

                boleta.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return boleta;
        }

        public static bool Update(Boleta boleta)
        {
            bool Result = false;
            try
            {
                var Query = BoletaQuerys.Update(boleta);
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

        public static List<Boleta> GetReporteFechas(string del, string hasta, bool onlyActives = true)
        {
            List<Boleta> ListBoleta = new List<Boleta>();
            try
            {
                var Query = BoletaQuerys.GetReporteFechas(del, hasta, onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListBoleta = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListBoleta;
        }
    }
}
