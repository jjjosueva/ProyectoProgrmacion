// ProyectoProgrmacion/Models/Pago.cs
namespace ProyectoProgrmacion.Models
{
    public class Pago
    {
        public decimal Cantidad { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
