using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Entities
{
    public class Visita
    {
        public int Id { get; set; }
        public int EstadoId { get; set; }
        public bool EsActivo { get; set; }

        public Catalogo Estado { get; set; }
        public Boleta Boleta { get; set; }
    }
}
