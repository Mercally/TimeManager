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
using TimeManager.Common.Enums;

namespace TimeManager.BL.Entities
{
    public class CatalogoBL
    {
        private static List<Catalogo> GetData(DataTable QueryResult)
        {
            List<Catalogo> ListCatalogo = new List<Catalogo>();
            try
            {
                foreach (DataRow Row in QueryResult.Rows)
                {
                    ListCatalogo.Add(new Catalogo()
                    {
                        Id = Row.IsNull("Id") ? 0 : Convert.ToInt32(Row["Id"]),
                        Nombre = Row.IsNull("Nombre") ? "" : Convert.ToString(Row["Nombre"]),
                        EsActivo = Row.IsNull("EsActivo") ? false : Convert.ToBoolean(Row["EsActivo"]),
                        FechaRegistro = Row.IsNull("FechaRegistro") ? DateTime.Now : Convert.ToDateTime(Row["FechaRegistro"])
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListCatalogo;
        }

        public static List<Catalogo> GetList(CatalogoEnum tableName, bool onlyActives = true)
        {
            List<Catalogo> ListCatalogo = new List<Catalogo>();
            try
            {
                var Query = CatalogoQuerys.GetList(tableName.ToString(), onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                ListCatalogo = GetData(Result);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return ListCatalogo;
        }

        public static Catalogo GetById(int id, CatalogoEnum tableName, bool onlyActives = true)
        {
            Catalogo Catalogo = null;
            try
            {
                var Query = CatalogoQuerys.GetById(id, tableName.ToString(), onlyActives);
                var Result = Commands.ExecuteQuery(Query);

                Catalogo = GetData(Result).First();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Catalogo;
        }

        public static Catalogo Create(Catalogo catalogo, CatalogoEnum tableName)
        {
            try
            {
                var Query = CatalogoQuerys.Create(catalogo, tableName.ToString());
                var Result = Convert.ToInt32(Commands.ExecuteScalar(Query));

                catalogo.Id = Result;
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return catalogo;
        }

        public static bool Update(Catalogo catalogo, CatalogoEnum tableName)
        {
            bool Result = false;
            try
            {
                var Query = CatalogoQuerys.Update(catalogo, tableName.ToString());
                Result = Commands.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogError(ex);
            }
            return Result;
        }

        public static bool Delete(int id, CatalogoEnum tableName, bool removePhysical = false)
        {
            bool Result = false;
            try
            {
                var Query = CatalogoQuerys.Delete(id, tableName.ToString(), removePhysical);
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
