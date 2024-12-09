// ProyectoProgrmacion/ViewModels/EncabezadoPedidosViewModel.cs
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
    public class EncabezadoPedidosViewModel : INotifyPropertyChanged
    {
        private readonly PedidoService _pedidoService;
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<EncabezadoPedido> _encabezadosList;
        public ObservableCollection<EncabezadoPedido> EncabezadosList
        {
            get => _encabezadosList;
            set
            {
                _encabezadosList = value;
                OnPropertyChanged();
            }
        }

        private EncabezadoPedido _nuevoEncabezado;
        public EncabezadoPedido NuevoEncabezado
        {
            get => _nuevoEncabezado;
            set
            {
                _nuevoEncabezado = value;
                OnPropertyChanged();
            }
        }

        // Comandos
        public Command AgregarEncabezadoCommand { get; }
        public Command<EncabezadoPedido> EditarEncabezadoCommand { get; }
        public Command<EncabezadoPedido> EliminarEncabezadoCommand { get; }
        public Command NavegarSiguienteCommand { get; }

        public EncabezadoPedidosViewModel(PedidoService pedidoService, IServiceProvider serviceProvider)
        {
            _pedidoService = pedidoService;
            _serviceProvider = serviceProvider;

            EncabezadosList = new ObservableCollection<EncabezadoPedido>();
            NuevoEncabezado = new EncabezadoPedido(); // Inicialización

            AgregarEncabezadoCommand = new Command(async () => await AgregarEncabezado());
            EditarEncabezadoCommand = new Command<EncabezadoPedido>(async (encabezado) => await EditarEncabezado(encabezado));
            EliminarEncabezadoCommand = new Command<EncabezadoPedido>(async (encabezado) => await EliminarEncabezado(encabezado));
            NavegarSiguienteCommand = new Command(async () => await NavegarSiguiente());

            CargarEncabezados();
        }

        private async void CargarEncabezados()
        {
            var lista = await _pedidoService.CargarEncabezadosAsync();
            foreach (var encabezado in lista)
            {
                EncabezadosList.Add(encabezado);
            }
        }

        private async Task AgregarEncabezado()
        {
            // Depuración: Imprimir los valores ingresados
            System.Diagnostics.Debug.WriteLine($"Titulo: {NuevoEncabezado.Titulo}");
            System.Diagnostics.Debug.WriteLine($"Descripcion: {NuevoEncabezado.Descripcion}");
            System.Diagnostics.Debug.WriteLine($"FechaEntrega: {NuevoEncabezado.FechaEntrega}");

            // Validación de campos con mensajes específicos
            if (NuevoEncabezado == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El encabezado del pedido no puede ser nulo.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoEncabezado.Titulo))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Título' es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevoEncabezado.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Descripción' es obligatorio.", "OK");
                return;
            }

            if (NuevoEncabezado.FechaEntrega == default)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione una fecha de entrega válida.", "OK");
                return;
            }

            // Agregar el encabezado a la lista
            EncabezadosList.Add(NuevoEncabezado);

            // Guardar en el servicio
            await _pedidoService.GuardarEncabezadosAsync(new List<EncabezadoPedido>(EncabezadosList));
            await Application.Current.MainPage.DisplayAlert("Éxito", "Encabezado de pedido agregado exitosamente.", "OK");

            // Resetear campos
            NuevoEncabezado = new EncabezadoPedido();
        }

        private async Task EditarEncabezado(EncabezadoPedido encabezado)
        {
            if (encabezado != null)
            {
                // Lógica de edición (puedes implementar más funcionalidad aquí)
                await _pedidoService.GuardarEncabezadosAsync(new List<EncabezadoPedido>(EncabezadosList));
                await Application.Current.MainPage.DisplayAlert("Éxito", "Encabezado de pedido editado exitosamente.", "OK");
            }
        }

        private async Task EliminarEncabezado(EncabezadoPedido encabezado)
        {
            if (encabezado != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Deseas eliminar el encabezado '{encabezado.Titulo}'?", "Sí", "No");
                if (confirm)
                {
                    EncabezadosList.Remove(encabezado);
                    await _pedidoService.GuardarEncabezadosAsync(new List<EncabezadoPedido>(EncabezadosList));
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Encabezado de pedido eliminado exitosamente.", "OK");
                }
            }
        }

        private async Task NavegarSiguiente()
        {
            // Instanciar la página a través de DI
            var piezasPage = _serviceProvider.GetService<PiezasPage>();
            if (piezasPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(piezasPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo navegar a PiezasPage.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
