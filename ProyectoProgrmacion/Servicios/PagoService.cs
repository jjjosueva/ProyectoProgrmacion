using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProyectoProgrmacion.Models;

namespace ProyectoProgrmacion.Servicios
{
    public class PagoService
    {
        private static string ObtenerRutaArchivo()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pagos.json");
        }

        public static void GuardarPagos(List<Pago> pagos)
        {
            string archivo = ObtenerRutaArchivo();
            string pagosJson = JsonConvert.SerializeObject(pagos);
            File.WriteAllText(archivo, pagosJson);
        }

        public static List<Pago> CargarPagos()
        {
            string archivo = ObtenerRutaArchivo();
            if (File.Exists(archivo))
            {
                string pagosJson = File.ReadAllText(archivo);
                return JsonConvert.DeserializeObject<List<Pago>>(pagosJson);
            }
            return new List<Pago>();
        }
    }
}
