using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProyectoProgrmacion.Servicios
{
    public class ArchivoService
    {
        private static string ObtenerRutaArchivo()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pedidos.json");
        }

        public static void GuardarPedidos(List<Pedido> pedidosList)
        {
            string archivo = ObtenerRutaArchivo();
            string pedidosJson = JsonConvert.SerializeObject(pedidosList);
            File.WriteAllText(archivo, pedidosJson);
        }

        public static List<Pedido> CargarPedidos()
        {
            string archivo = ObtenerRutaArchivo();
            if (File.Exists(archivo))
            {
                string pedidosJson = File.ReadAllText(archivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(pedidosJson);
            }
            return new List<Pedido>();
        }
    }
}
