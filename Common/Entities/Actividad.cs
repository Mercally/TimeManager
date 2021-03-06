﻿using System;
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
        [DisplayName("Fecha registro")]
        public DateTime FechaRegistro { get; set; }

        [HiddenInput]
        public Catalogo Estado { get; set; }

        [HiddenInput]
        public Boleta Boleta { get; set; }

        [Required]
        [DisplayName("Fecha de actividad")]
        public DateTime FechaActividad { get; set; }

        [Required]
        [DisplayName("Tiempo de actividad")]
        [Description("Ninguna")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Debe ingresar una cantidad decimal, máximo dos decimales.")]
        [Range(0.15, 24)]
        public decimal TiempoActividad { get; set; }
    }
}
