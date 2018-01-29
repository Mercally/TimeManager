using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Common.Entities
{
    public class Boleta
    {
        public int Id { get; set; }
        public string NumeroBoleta { get; set; }
        public DateTime FechaEntrada { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan TiempoEfectivo { get; set; }
        public int TiempoInvertidoEn { get; set; }
        public int ProyectoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioId { get; set; }
        public int DepartamentoId { get; set; }
        public bool EsActivo { get; set; }

        public Catalogo TiempoInvertido { get; set; }
        public Proyecto Proyecto { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public Catalogo Departamento { get; set; }
        public List<Actividad> Actividades { get; set; }
    }
}
