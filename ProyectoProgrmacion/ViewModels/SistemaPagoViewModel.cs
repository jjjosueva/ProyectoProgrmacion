// ProyectoProgrmacion/ViewModels/SistemaPagoViewModel.cs
using ProyectoProgrmacion.Models;
using ProyectoProgrmacion.Services;
using ProyectoProgrmacion.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

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

            PagosList = new ObservableCollection<Pago>();
            NuevoPago = new Pago(); // Inicialización

            AgregarPagoCommand = new Command(async () => await AgregarPago());
            EditarPagoCommand = new Command<Pago>(async (pago) => await EditarPago(pago));
            EliminarPagoCommand = new Command<Pago>(async (pago) => await EliminarPago(pago));


            CargarPagos();
        }

        private async void CargarPagos()
        {
            var lista = await _pedidoService.CargarPagosAsync();
            foreach (var pago in lista)
            {
                PagosList.Add(pago);
            }
        }

        private async Task AgregarPago()
        {
            // Depuración: Imprimir los valores ingresados
            System.Diagnostics.Debug.WriteLine($"Cantidad: {NuevoPago.Cantidad}");
            System.Diagnostics.Debug.WriteLine($"MetodoPago: {NuevoPago.MetodoPago}");
            System.Diagnostics.Debug.WriteLine($"FechaPago: {NuevoPago.FechaPago}");

            // Validación de campos con mensajes específicos
            if (NuevoPago == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El pago no puede ser nulo.", "OK");
                return;
            }

            if (NuevoPago.Cantidad <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La cantidad debe ser mayor a cero.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoPago.MetodoPago))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Método de Pago' es obligatorio.", "OK");
                return;
            }

            if (NuevoPago.FechaPago == default)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione una fecha de pago válida.", "OK");
                return;
            }

            // Agregar el pago a la lista
            PagosList.Add(NuevoPago);

            // Guardar en el servicio
            await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));
            await Application.Current.MainPage.DisplayAlert("Éxito", "Pago agregado exitosamente.", "OK");

            // Resetear campos
            NuevoPago = new Pago();
        }

        private async Task EditarPago(Pago pago)
        {
            if (pago != null)
            {
                // Lógica de edición (puedes implementar más funcionalidad aquí)
                await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));
                await Application.Current.MainPage.DisplayAlert("Éxito", "Pago editado exitosamente.", "OK");
            }
        }

        private async Task EliminarPago(Pago pago)
        {
            if (pago != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Deseas eliminar el pago de '{pago.Cantidad:C}'?", "Sí", "No");
                if (confirm)
                {
                    PagosList.Remove(pago);
                    await _pedidoService.GuardarPagosAsync(new List<Pago>(PagosList));
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pago eliminado exitosamente.", "OK");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
