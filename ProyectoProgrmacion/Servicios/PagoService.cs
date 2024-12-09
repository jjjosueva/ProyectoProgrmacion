// ProyectoProgrmacion/Services/PagoService.cs
using Newtonsoft.Json;
using ProyectoProgrmacion.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace ProyectoProgrmacion.Services
{
    public class PagoService
    {
        private readonly string archivoPagos = Path.Combine(FileSystem.AppDataDirectory, "pagos.json");

        // Cargar pagos desde el archivo JSON
        public async Task<List<Pago>> CargarPagosAsync()
        {
            if (File.Exists(archivoPagos))
            {
                var json = await File.ReadAllTextAsync(archivoPagos);
                return JsonConvert.DeserializeObject<List<Pago>>(json) ?? new List<Pago>();
            }
            return new List<Pago>();
        }

        // Guardar pagos en el archivo JSON
        public async Task GuardarPagosAsync(List<Pago> pagos)
        {
            var json = JsonConvert.SerializeObject(pagos, Formatting.Indented);
            await File.WriteAllTextAsync(archivoPagos, json);
        }
    }
}
