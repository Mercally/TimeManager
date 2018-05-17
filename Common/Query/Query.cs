using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Common.Query
{
    public class Query
    {
        /// <summary>
        /// Consulta en forma de cadena T-SQL
        /// </summary>
        public string RawQuery { get; set; }
        /// <summary>
        /// Lista de parámetros según nombre en RawQuery
        /// </summary>
        public List<SqlParameter> Parameters { get; set; }
        /// <summary>
        /// Tipo de consulta CRUD
        /// </summary>
        public TypeCrud Type { get; set; }
        /// <summary>
        /// Determina si la consulta tiene errores
        /// </summary>
        public bool HasError { get; set; }

        public Query()
        {
            this.HasError = false;
            this.Parameters = new List<SqlParameter>();
        }
    }
}
