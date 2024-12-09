// ProyectoProgrmacion/Models/Pedido.cs
namespace ProyectoProgrmacion.Models
{
    public class Pedido
    {
        public string PedidoNombre { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DetallesPieza { get; set; }
    }
}
