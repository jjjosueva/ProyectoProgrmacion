using System;
using System.Xml.Serialization;

namespace ProyectoProgrmacion
{
    [XmlRoot("Pedidos")]
    public class Pedido
    {
        [XmlElement("PedidoNombre")]
        public string PedidoNombre { get; set; }

        [XmlElement("NombreCliente")]
        public string NombreCliente { get; set; }

        [XmlElement("FechaEntrega")]
        public string FechaEntrega { get; set; }

        [XmlElement("DetallesPieza")]
        public string DetallesPieza { get; set; }
    }
}
