// ProyectoProgrmacion/ViewModels/DetallesPedidosViewModel.cs
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
    public class DetallesPedidosViewModel : INotifyPropertyChanged
    {
        private readonly PedidoService _pedidoService;
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<Pedido> _pedidosList;
        public ObservableCollection<Pedido> PedidosList
        {
            get => _pedidosList;
            set
            {
                _pedidosList = value;
                OnPropertyChanged();
            }
        }

        private Pedido _nuevoPedido;
        public Pedido NuevoPedido
        {
            get => _nuevoPedido;
            set
            {
                _nuevoPedido = value;
                OnPropertyChanged();
            }
        }

        private Pedido _pedidoSeleccionado;
        public Pedido PedidoSeleccionado
        {
            get => _pedidoSeleccionado;
            set
            {
                _pedidoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        // Comandos
        public Command AgregarPedidoCommand { get; }
        public Command<Pedido> EditarPedidoCommand { get; }
        public Command<Pedido> EliminarPedidoCommand { get; }
        public Command NavegarSiguienteCommand { get; }

        public DetallesPedidosViewModel(PedidoService pedidoService, IServiceProvider serviceProvider)
        {
            _pedidoService = pedidoService;
            _serviceProvider = serviceProvider;

            PedidosList = new ObservableCollection<Pedido>();
            NuevoPedido = new Pedido(); // Inicialización aquí

            AgregarPedidoCommand = new Command(async () => await AgregarPedido());
            EditarPedidoCommand = new Command<Pedido>(async (pedido) => await EditarPedido(pedido));
            EliminarPedidoCommand = new Command<Pedido>(async (pedido) => await EliminarPedido(pedido));
            NavegarSiguienteCommand = new Command(async () => await NavegarSiguiente());

            CargarPedidos();
        }

        private async void CargarPedidos()
        {
            var lista = await _pedidoService.CargarPedidosAsync();
            foreach (var pedido in lista)
            {
                PedidosList.Add(pedido);
            }
        }

        private async Task AgregarPedido()
        {
            // Depuración: Imprimir los valores ingresados
            System.Diagnostics.Debug.WriteLine($"PedidoNombre: {NuevoPedido.PedidoNombre}");
            System.Diagnostics.Debug.WriteLine($"NombreCliente: {NuevoPedido.NombreCliente}");
            System.Diagnostics.Debug.WriteLine($"DetallesPieza: {NuevoPedido.DetallesPieza}");
            System.Diagnostics.Debug.WriteLine($"FechaEntrega: {NuevoPedido.FechaEntrega}");

            // Validación de campos con mensajes específicos
            if (NuevoPedido == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El pedido no puede ser nulo.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoPedido.PedidoNombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Pedido Personalizado' es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoPedido.NombreCliente))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Nombre del Cliente' es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoPedido.DetallesPieza))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Detalles de la Pieza' es obligatorio.", "OK");
                return;
            }

            if (NuevoPedido.FechaEntrega == default)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione una fecha de entrega válida.", "OK");
                return;
            }

            // Agregar el pedido a la lista
            PedidosList.Add(NuevoPedido);

            // Guardar en el servicio
            await _pedidoService.GuardarPedidosAsync(new List<Pedido>(PedidosList));
            await Application.Current.MainPage.DisplayAlert("Éxito", "Pedido agregado exitosamente.", "OK");

            // Resetear campos
            NuevoPedido = new Pedido();
        }

        private async Task EditarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                // Lógica de edición (puedes implementar más funcionalidad aquí)
                await _pedidoService.GuardarPedidosAsync(new List<Pedido>(PedidosList));
                await Application.Current.MainPage.DisplayAlert("Éxito", "Pedido editado exitosamente.", "OK");
            }
        }

        private async Task EliminarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Deseas eliminar el pedido '{pedido.PedidoNombre}'?", "Sí", "No");
                if (confirm)
                {
                    PedidosList.Remove(pedido);
                    await _pedidoService.GuardarPedidosAsync(new List<Pedido>(PedidosList));
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pedido eliminado exitosamente.", "OK");
                }
            }
        }

        private async Task NavegarSiguiente()
        {
            // Instanciar la página a través de DI
            var encabezadoPedidosPage = _serviceProvider.GetService<EncabezadoPedidosPage>();
            if (encabezadoPedidosPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(encabezadoPedidosPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo navegar a EncabezadoPedidosPage.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
