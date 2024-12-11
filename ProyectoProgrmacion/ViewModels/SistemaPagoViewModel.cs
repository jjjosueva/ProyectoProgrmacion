using ProyectoProgrmacion.Models;
using ProyectoProgrmacion.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoProgrmacion.ViewModels
{
    public class SistemaPagoViewModel : INotifyPropertyChanged
    {
        private readonly PedidoService _pedidoService;
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<Pago> _pagosList;
        public ObservableCollection<Pago> PagosList
        {
            get => _pagosList;
            set
            {
                _pagosList = value;
                OnPropertyChanged();
            }
        }

        private Pago _nuevoPago;
        public Pago NuevoPago
        {
            get => _nuevoPago;
            set
            {
                _nuevoPago = value;
                OnPropertyChanged();
            }
        }

        // Comandos
        public Command AgregarPagoCommand { get; }
        public Command<Pago> EditarPagoCommand { get; }
        public Command<Pago> EliminarPagoCommand { get; }

        public SistemaPagoViewModel(PedidoService pedidoService, IServiceProvider serviceProvider)
        {
            _pedidoService = pedidoService;
            _serviceProvider = serviceProvider;

            // Inicializar listas y comandos
            PagosList = new ObservableCollection<Pago>();
            NuevoPago = new Pago();

            AgregarPagoCommand = new Command(async () => await AgregarPago());
            EditarPagoCommand = new Command<Pago>(async (pago) => await EditarPago(pago));
            EliminarPagoCommand = new Command<Pago>(async (pago) => await EliminarPago(pago));

            // Cargar datos iniciales
            CargarPagos();
        }

        private async void CargarPagos()
        {
            try
            {
                var lista = await _pedidoService.CargarPagosAsync();
                foreach (var pago in lista)
                {
                    PagosList.Add(pago);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar los pagos: {ex.Message}", "OK");
            }
        }

        private async Task AgregarPago()
        {
            try
            {
                // Validación de los campos del pago, incluida la verificación de 'Cantidad'
                if (NuevoPago == null || string.IsNullOrWhiteSpace(NuevoPago.MetodoPago) ||
                    !EsCantidadValida(NuevoPago.Cantidad) || NuevoPago.FechaPago == default)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Por favor, complete todos los campos correctamente.", "OK");
                    return;
                }

                // Agregar nuevo pago
                PagosList.Add(NuevoPago);
                await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));

                // Confirmar acción
                await Application.Current.MainPage.DisplayAlert("Éxito", "Pago agregado correctamente.", "OK");

                // Resetear el modelo para el próximo ingreso
                NuevoPago = new Pago();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar el pago: {ex.Message}", "OK");
            }
        }

        // Método para validar que la cantidad sea un valor decimal válido
        private bool EsCantidadValida(decimal cantidad)
        {
            return cantidad > 0;  // Asegura que la cantidad sea mayor a 0, ajusta la validación según sea necesario
        }

        private async Task EditarPago(Pago pago)
        {
            try
            {
                if (pago != null)
                {
                    await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pago editado correctamente.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al editar el pago: {ex.Message}", "OK");
            }
        }

        private async Task EliminarPago(Pago pago)
        {
            try
            {
                if (pago != null)
                {
                    bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Estás seguro de eliminar este pago?", "Sí", "No");
                    if (confirm)
                    {
                        PagosList.Remove(pago);
                        await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Pago eliminado correctamente.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar el pago: {ex.Message}", "OK");
            }
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
