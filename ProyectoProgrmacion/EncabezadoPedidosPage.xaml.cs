using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.Servicios;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace ProyectoProgrmacion
{
    public partial class EncabezadoPedidosPage : ContentPage
    {
        private List<Pedido> pedidosList = new List<Pedido>();
        private Pedido pedidoSeleccionado;
        private const string archivoPedidos = "encabezado_pedido.json";

        public ObservableCollection<Pedido> Pedidos { get; set; }

        public EncabezadoPedidosPage()
        {
            InitializeComponent();
            pedidosList = EncabezadoPedidoService.CargarEncabezados();
            Pedidos = new ObservableCollection<Pedido>(pedidosList); 

            PedidosListView.ItemsSource = Pedidos;
        }

        private void OnSavePedidoClicked(object sender, EventArgs e)
        {
            string pedidoEncabezado = PedidoEntry.Text;
            DateTime fechaEntrega = FechaEntregaDatePicker.Date;
            string detallesPieza = DetallesPiezaEntry.Text;

            if (string.IsNullOrEmpty(pedidoEncabezado) || string.IsNullOrEmpty(detallesPieza))
            {
                DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            Pedido nuevoPedido = new Pedido
            {
                PedidoNombre = pedidoEncabezado,
                FechaEntrega = fechaEntrega.ToString("yyyy-MM-dd"),
                DetallesPieza = detallesPieza
            };

            Pedidos.Add(nuevoPedido);
            EncabezadoPedidoService.GuardarEncabezados(new List<Pedido>(Pedidos));

            PedidoEntry.Text = string.Empty;
            DetallesPiezaEntry.Text = string.Empty;
            FechaEntregaDatePicker.Date = DateTime.Now;
            DisplayAlert("Pedido Guardado", $"Pedido '{pedidoEncabezado}' guardado exitosamente.", "OK");
        }

        private void OnPedidoTapped(object sender, ItemTappedEventArgs e)
        {
            pedidoSeleccionado = e.Item as Pedido;

            if (pedidoSeleccionado != null)
            {
                PedidoEntry.Text = pedidoSeleccionado.PedidoNombre;
                FechaEntregaDatePicker.Date = DateTime.Parse(pedidoSeleccionado.FechaEntrega);
                DetallesPiezaEntry.Text = pedidoSeleccionado.DetallesPieza;
            }
        }

        private void OnEditPedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidoSeleccionado.PedidoNombre = PedidoEntry.Text;
                pedidoSeleccionado.FechaEntrega = FechaEntregaDatePicker.Date.ToString("yyyy-MM-dd");
                pedidoSeleccionado.DetallesPieza = DetallesPiezaEntry.Text;

                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = Pedidos;

                EncabezadoPedidoService.GuardarEncabezados(new List<Pedido>(Pedidos));

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
                Pedidos.Remove(pedidoSeleccionado);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = Pedidos;

                EncabezadoPedidoService.GuardarEncabezados(new List<Pedido>(Pedidos));

                DisplayAlert("Pedido Eliminado", "El pedido ha sido eliminado correctamente.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pedido para eliminar.", "OK");
            }
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PiezasPage());
        }
    }
}
