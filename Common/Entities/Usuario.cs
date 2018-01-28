using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TimeManager.Entities.Extensions;

namespace TimeManager.Common.Entities
{
    public class Usuario
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Debe ingresar más de {2} y menos de {1} caracteres.", MinimumLength = 10)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Debe ingresar más de {2} y menos de {1} caracteres.", MinimumLength = 10)]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }
        [DisplayName("Repetir contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Contrasenia", ErrorMessage = "Las contraseñas no coinciden")]
        public string _Contrasenia { get; set; }
        [HiddenInput]
        [DisplayName("Fecha registrado")]
        public DateTime FechaRegistro { get; set; }
        [HiddenInput]
        [DisplayName("Estado")]
        [BooleanDisplayValuesAsEnableDisableAttribute]
        public bool EsActivo { get; set; }
    }
}
