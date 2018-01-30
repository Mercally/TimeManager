using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TimeManager.Common.Entities
{
    public class Cliente
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [MaxLength(99)]
        [DisplayName("Nombre cliente")]
        public string Nombre { get; set; }
        [HiddenInput]
        [DisplayName("Fecha registro")]
        public DateTime FechaRegistro { get; set; }
        [HiddenInput]
        [DisplayName("Estado")]
        public bool EsActivo { get; set; }
    }
}
