using System;
using System.Xml.Serialization;

namespace ProyectoProgrmacion
{
    [XmlRoot("Pedidos")]
    public class Pedido
    {
        public string PedidoNombre { get; set; }

        public string NombreCliente { get; set; }

        public string FechaEntrega { get; set; }

        public string DetallesPieza { get; set; }
    }
}
