// ProyectoProgrmacion/Services/PiezaService.cs
using Newtonsoft.Json;
using ProyectoProgrmacion.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace ProyectoProgrmacion.Services
{
    public class PiezaService
    {
        private readonly string archivoPiezas = Path.Combine(FileSystem.AppDataDirectory, "piezas.json");

        // Cargar piezas desde el archivo JSON
        public async Task<List<Pieza>> CargarPiezasAsync()
        {
            if (File.Exists(archivoPiezas))
            {
                var json = await File.ReadAllTextAsync(archivoPiezas);
                return JsonConvert.DeserializeObject<List<Pieza>>(json) ?? new List<Pieza>();
            }
            return new List<Pieza>();
        }

        // Guardar piezas en el archivo JSON
        public async Task GuardarPiezasAsync(List<Pieza> piezas)
        {
            var json = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            await File.WriteAllTextAsync(archivoPiezas, json);
        }
    }
}
