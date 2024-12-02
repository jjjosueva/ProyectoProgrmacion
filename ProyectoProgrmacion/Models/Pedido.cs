
namespace ProyectoProgrmacion
{
    public class Pedido
    {
        public string PedidoNombre { get; set; }
        public string FechaPedido { get; set; }
        public List<Pieza> Piezas { get; internal set; }
    }
}
