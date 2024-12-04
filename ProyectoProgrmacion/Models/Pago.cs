using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoProgrmacion.Models
{
    public class Pago
    {
        public string MetodoPago { get; set; }
        public string Cantidad { get; set; }
        public DateTime FechaPago { get; set; }
    }

}
