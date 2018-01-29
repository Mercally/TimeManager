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
    public class Actividad
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [DisplayName("Descripción de la activdad")]
        [MaxLength(330)]
        public string Descripcion { get; set; }
        [HiddenInput]
        public int BoletaId { get; set; }
        [Required]
        [DisplayName("Estado")]
        public int EstadoId { get; set; }
        [HiddenInput]
        public bool EsActivo { get; set; }
        [HiddenInput]
        public DateTime FechaRegistro { get; set; }

        [HiddenInput]
        public Catalogo Estado { get; set; }
        [HiddenInput]
        public Boleta Boleta { get; set; }

        [DisplayName("Fecha de actividad")]
        public DateTime FechaActividad { get; set; }
        [DisplayName("Tiempo de actividad")]
        public TimeSpan TiempoActividad { get; set; }
    }
}
