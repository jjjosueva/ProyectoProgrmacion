using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Storage;
using ProyectoProgrmacion.Servicios; 

namespace ProyectoProgrmacion
{
    public partial class DetallesPedidosPage : ContentPage
    {
        private List<Pedido> pedidosList = new List<Pedido>(); 
        private Pedido pedidoSeleccionado;
        private const string archivoPedidos = "pedidos.json"; 

        public DetallesPedidosPage()
        {
            InitializeComponent();
            pedidosList = ArchivoService.CargarPedidos();

            PedidosListView.ItemsSource = pedidosList;
            PedidosListView.ItemsSource = pedidosList;
            CargarPedidos(); 
        }

        private async void CargarPedidos()
        {
            try
            {
                var rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, archivoPedidos); 
                if (File.Exists(rutaArchivo))
                {
                    var json = await File.ReadAllTextAsync(rutaArchivo);
                    var pedidosCargados = JsonConvert.DeserializeObject<List<Pedido>>(json);
                    if (pedidosCargados != null)
                    {
                        pedidosList = pedidosCargados;
                        PedidosListView.ItemsSource = pedidosList;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron cargar los pedidos: {ex.Message}", "OK");
            }
        }

        private async void GuardarPedidos()
        {
            try
            {
                var rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, archivoPedidos); 
                var json = JsonConvert.SerializeObject(pedidosList, Formatting.Indented); 
                await File.WriteAllTextAsync(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron guardar los pedidos: {ex.Message}", "OK");
            }
        }

        private void OnAddPedidoClicked(object sender, EventArgs e)
        {
            string pedidoPersonalizado = PedidoPersonalizadoEntry.Text;
            string pedidoPredefinido = PedidosPicker.SelectedItem?.ToString();
            string nombreCliente = NombreClienteEntry.Text;
            string fechaEntrega = FechaEntregaDatePicker.Date.ToString("yyyy-MM-dd");
            string detallesPieza = DetallesPiezaEntry.Text;

            if (!string.IsNullOrEmpty(pedidoPersonalizado) || !string.IsNullOrEmpty(pedidoPredefinido))
            {
                var nuevoPedido = new Pedido
                {
                    PedidoNombre = !string.IsNullOrEmpty(pedidoPersonalizado) ? pedidoPersonalizado : pedidoPredefinido,
                    NombreCliente = nombreCliente,
                    FechaEntrega = fechaEntrega,
                    DetallesPieza = detallesPieza
                };

                pedidosList.Add(nuevoPedido);
                ArchivoService.GuardarPedidos(pedidosList);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                PedidoPersonalizadoEntry.Text = string.Empty;
                PedidosPicker.SelectedIndex = -1;
                NombreClienteEntry.Text = string.Empty;
                FechaEntregaDatePicker.Date = DateTime.Now;
                DetallesPiezaEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Por favor ingrese o seleccione un pedido", "OK");
            }
        }

        private void OnPedidoTapped(object sender, ItemTappedEventArgs e)
        {
            pedidoSeleccionado = e.Item as Pedido;

            if (pedidoSeleccionado != null)
            {
                PedidoPersonalizadoEntry.Text = pedidoSeleccionado.PedidoNombre;
                NombreClienteEntry.Text = pedidoSeleccionado.NombreCliente;
                FechaEntregaDatePicker.Date = DateTime.Parse(pedidoSeleccionado.FechaEntrega);
                DetallesPiezaEntry.Text = pedidoSeleccionado.DetallesPieza;
            }
        }

        private void OnEditPedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidoSeleccionado.PedidoNombre = PedidoPersonalizadoEntry.Text;
                pedidoSeleccionado.NombreCliente = NombreClienteEntry.Text;
                pedidoSeleccionado.FechaEntrega = FechaEntregaDatePicker.Date.ToString("yyyy-MM-dd");
                pedidoSeleccionado.DetallesPieza = DetallesPiezaEntry.Text;

                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                GuardarPedidos();

                DisplayAlert("Pedido Editado", "El pedido ha sido actualizado.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pedido para editar.", "OK");
            }
        }

        private void OnDeletePedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidosList.Remove(pedidoSeleccionado);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                GuardarPedidos();
                DisplayAlert("Pedido Eliminado", "El pedido ha sido eliminado correctamente.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pedido para eliminar.", "OK");
            }
        }

        private void OnPedidoPredefinidoSelected(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var selectedPedido = picker.SelectedItem?.ToString();
            DisplayAlert("Pedido Seleccionado", $"Has seleccionado: {selectedPedido}", "OK");
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EncabezadoPedidosPage());
        }
    }
}
