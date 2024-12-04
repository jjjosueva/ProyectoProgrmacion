using ProyectoProgrmacion.Servicios;
using ProyectoProgrmacion.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;

namespace ProyectoProgrmacion
{
    public partial class SistemaPagoPage : ContentPage
    {
        private List<Pago> pagosList = new List<Pago>();
        private Pago pagoSeleccionado;
        private string metodoPagoSeleccionado;  
        private const string archivoPagos = "pagos.json";

        public SistemaPagoPage()
        {
            InitializeComponent();
            pagosList = PagoService.CargarPagos(); 
            BindingContext = this;
            PagosListView.ItemsSource = pagosList; 
        }

        private async void CargarPagos()
        {
            try
            {
                var rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, archivoPagos); 
                if (File.Exists(rutaArchivo))
                {
                    var json = await File.ReadAllTextAsync(rutaArchivo);
                    var pagosCargados = JsonConvert.DeserializeObject<List<Pago>>(json); 
                    if (pagosCargados != null)
                    {
                        pagosList = pagosCargados;
                        PagosListView.ItemsSource = pagosList; 
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron cargar los pagos: {ex.Message}", "OK");
            }
        }

        private async void GuardarPagos()
        {
            try
            {
                var rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, archivoPagos); 
                var json = JsonConvert.SerializeObject(pagosList, Formatting.Indented); 
                await File.WriteAllTextAsync(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudieron guardar los pagos: {ex.Message}", "OK");
            }
        }

        private void OnMetodoPagoSelected(object sender, EventArgs e)
        {
            metodoPagoSeleccionado = MetodoPagoPicker.SelectedItem?.ToString();
        }

        private void OnPagoClicked(object sender, EventArgs e)
        {
            string cantidad = PagoEntry.Text;

            if (string.IsNullOrEmpty(cantidad))
            {
                DisplayAlert("Error", "Por favor ingrese una cantidad a pagar", "OK");
                return;
            }

            if (string.IsNullOrEmpty(metodoPagoSeleccionado))
            {
                DisplayAlert("Error", "Por favor seleccione un método de pago", "OK");
                return;
            }

            DateTime fechaPago = FechaPagoPicker.Date;

            Pago nuevoPago = new Pago
            {
                MetodoPago = metodoPagoSeleccionado,
                Cantidad = cantidad,
                FechaPago = fechaPago
            };

            pagosList.Add(nuevoPago);
            GuardarPagos();

            PagosListView.ItemsSource = null;
            PagosListView.ItemsSource = pagosList;

            PagoEntry.Text = string.Empty;
            MetodoPagoPicker.SelectedIndex = -1;
            FechaPagoPicker.Date = DateTime.Today;

            DisplayAlert("Pago Realizado", $"El pago de {cantidad} ha sido realizado con éxito", "OK");
        }

        private void OnPagoTapped(object sender, ItemTappedEventArgs e)
        {
            pagoSeleccionado = e.Item as Pago;

            if (pagoSeleccionado != null)
            {
                PagoEntry.Text = pagoSeleccionado.Cantidad;
                FechaPagoPicker.Date = pagoSeleccionado.FechaPago;
                MetodoPagoPicker.SelectedItem = pagoSeleccionado.MetodoPago;
            }
        }

        private void OnEditPagoClicked(object sender, EventArgs e)
        {
            if (pagoSeleccionado != null)
            {
                pagoSeleccionado.Cantidad = PagoEntry.Text;
                pagoSeleccionado.FechaPago = FechaPagoPicker.Date;
                pagoSeleccionado.MetodoPago = metodoPagoSeleccionado;

                GuardarPagos(); 
                DisplayAlert("Pago Editado", "El pago ha sido actualizado.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pago para editar.", "OK");
            }
        }

        private void OnDeletePagoClicked(object sender, EventArgs e)
        {
            if (pagoSeleccionado != null)
            {
                pagosList.Remove(pagoSeleccionado);
                GuardarPagos(); 
                DisplayAlert("Pago Eliminado", "El pago ha sido eliminado correctamente.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pago para eliminar.", "OK");
            }
        }
    }
}
