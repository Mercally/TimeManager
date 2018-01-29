using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TimeManager.Common.Entities
{
    public class Proyecto
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [HiddenInput]
        public DateTime FechaRegistro { get; set; }
        [HiddenInput]
        public bool EsActivo { get; set; }
    }
}
