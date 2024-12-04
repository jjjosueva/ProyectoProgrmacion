using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProyectoProgrmacion;

namespace ProyectoProgrmacion.Servicios
{
    public class EncabezadoPedidoService
    {
        private static string ObtenerRutaArchivo()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "encabezado_pedido.json");
        }
        public static void GuardarEncabezados(List<Pedido> pedidosList)
        {
            string archivo = ObtenerRutaArchivo();
            string pedidosJson = JsonConvert.SerializeObject(pedidosList); 
            File.WriteAllText(archivo, pedidosJson);  
        }
        public static List<Pedido> CargarEncabezados()
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
