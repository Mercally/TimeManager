using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TimeManager.Common.Entities
{
    public class Boleta
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [DisplayName("Numero Boleta")]
        public string NumeroBoleta { get; set; }
        [Required]
        [DisplayName("Fecha de entrada")]
        public DateTime FechaEntrada { get; set; }
        [Required]
        [DisplayName("Hora de entrada")]
        public TimeSpan HoraEntrada { get; set; }
        [Required]
        [DisplayName("Fecha de salida")]
        public DateTime FechaSalida { get; set; }
        [Required]
        [DisplayName("Hora de salida")]
        public TimeSpan HoraSalida { get; set; }
        [Required]
        [DisplayName("Tiempo efectivo")]
        public decimal TiempoEfectivo { get; set; }
        [Required]
        [DisplayName("Invertido en")]
        public int TiempoInvertidoEn { get; set; }
        [Required]
        [DisplayName("Proyecto")]
        public int ProyectoId { get; set; }
        [Required]
        [DisplayName("Cliente")]
        public int ClienteId { get; set; }
        [HiddenInput]
        public DateTime FechaRegistro { get; set; }
        [HiddenInput]
        public int UsuarioId { get; set; }
        [Required]
        public int DepartamentoId { get; set; }
        [HiddenInput]
        public bool EsActivo { get; set; }

        [HiddenInput]
        public Catalogo TiempoInvertido { get; set; }
        [HiddenInput]
        public Proyecto Proyecto { get; set; }
        [HiddenInput]
        public Cliente Cliente { get; set; }
        [HiddenInput]
        public Usuario Usuario { get; set; }
        [HiddenInput]
        public Catalogo Departamento { get; set; }
        [HiddenInput]
        public List<Actividad> Actividades { get; set; }
    }
}
