// ProyectoProgrmacion/Services/PedidoService.cs
using Newtonsoft.Json;
using ProyectoProgrmacion.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace ProyectoProgrmacion.Services
{
    public class PedidoService
    {
        private readonly string archivoPedidos = Path.Combine(FileSystem.AppDataDirectory, "pedidos.json");
        private readonly string archivoEncabezados = Path.Combine(FileSystem.AppDataDirectory, "encabezados_pedidos.json");
        private readonly string archivoPiezas = Path.Combine(FileSystem.AppDataDirectory, "piezas.json");
        private readonly string archivoPagos = Path.Combine(FileSystem.AppDataDirectory, "pagos.json");

        // Métodos para Pedidos (ya existentes)
        public async Task<List<Pedido>> CargarPedidosAsync()
        {
            if (File.Exists(archivoPedidos))
            {
                var json = await File.ReadAllTextAsync(archivoPedidos);
                return JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }

        public async Task GuardarPedidosAsync(List<Pedido> pedidos)
        {
            var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            await File.WriteAllTextAsync(archivoPedidos, json);
        }

        // Métodos para Encabezados de Pedidos (ya existentes)
        public async Task<List<EncabezadoPedido>> CargarEncabezadosAsync()
        {
            if (File.Exists(archivoEncabezados))
            {
                var json = await File.ReadAllTextAsync(archivoEncabezados);
                return JsonConvert.DeserializeObject<List<EncabezadoPedido>>(json) ?? new List<EncabezadoPedido>();
            }
            return new List<EncabezadoPedido>();
        }

        public async Task GuardarEncabezadosAsync(List<EncabezadoPedido> encabezados)
        {
            var json = JsonConvert.SerializeObject(encabezados, Formatting.Indented);
            await File.WriteAllTextAsync(archivoEncabezados, json);
        }

        // Métodos para Piezas (ya existentes)
        public async Task<List<Pieza>> CargarPiezasAsync()
        {
            if (File.Exists(archivoPiezas))
            {
                var json = await File.ReadAllTextAsync(archivoPiezas);
                return JsonConvert.DeserializeObject<List<Pieza>>(json) ?? new List<Pieza>();
            }
            return new List<Pieza>();
        }

        public async Task GuardarPiezasAsync(List<Pieza> piezas)
        {
            var json = JsonConvert.SerializeObject(piezas, Formatting.Indented);
            await File.WriteAllTextAsync(archivoPiezas, json);
        }

        // Métodos para Pagos
        public async Task<List<Pago>> CargarPagosAsync()
        {
            if (File.Exists(archivoPagos))
            {
                var json = await File.ReadAllTextAsync(archivoPagos);
                return JsonConvert.DeserializeObject<List<Pago>>(json) ?? new List<Pago>();
            }
            return new List<Pago>();
        }

        public async Task GuardarPagosAsync(List<Pago> pagos)
        {
            var json = JsonConvert.SerializeObject(pagos, Formatting.Indented);
            await File.WriteAllTextAsync(archivoPagos, json);
        }
    }
}
