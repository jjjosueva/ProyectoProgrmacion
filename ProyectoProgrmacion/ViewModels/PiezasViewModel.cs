// ProyectoProgrmacion/ViewModels/PiezasViewModel.cs
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
    public class PiezasViewModel : INotifyPropertyChanged
    {
        private readonly PedidoService _pedidoService;
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<Pieza> _piezasList;
        public ObservableCollection<Pieza> PiezasList
        {
            get => _piezasList;
            set
            {
                _piezasList = value;
                OnPropertyChanged();
            }
        }

        private Pieza _nuevaPieza;
        public Pieza NuevaPieza
        {
            get => _nuevaPieza;
            set
            {
                _nuevaPieza = value;
                OnPropertyChanged();
            }
        }

        // Comandos
        public Command AgregarPiezaCommand { get; }
        public Command<Pieza> EditarPiezaCommand { get; }
        public Command<Pieza> EliminarPiezaCommand { get; }

        public Command NavegarSiguienteCommand { get; }

        public PiezasViewModel(PedidoService pedidoService, IServiceProvider serviceProvider)
        {
            _pedidoService = pedidoService;
            _serviceProvider = serviceProvider;

            PiezasList = new ObservableCollection<Pieza>();
            NuevaPieza = new Pieza(); // Inicialización

            AgregarPiezaCommand = new Command(async () => await AgregarPieza());
            EditarPiezaCommand = new Command<Pieza>(async (pieza) => await EditarPieza(pieza));
            EliminarPiezaCommand = new Command<Pieza>(async (pieza) => await EliminarPieza(pieza));
            NavegarSiguienteCommand = new Command(async () => await NavegarSiguiente());

            CargarPiezas();
        }

        private async void CargarPiezas()
        {
            var lista = await _pedidoService.CargarPiezasAsync();
            foreach (var pieza in lista)
            {
                PiezasList.Add(pieza);
            }
        }

        private async Task AgregarPieza()
        {
            
            System.Diagnostics.Debug.WriteLine($"Nombre: {NuevaPieza.Nombre}");
            System.Diagnostics.Debug.WriteLine($"Descripcion: {NuevaPieza.Descripcion}");
            System.Diagnostics.Debug.WriteLine($"Precio: {NuevaPieza.Precio}");
            System.Diagnostics.Debug.WriteLine($"ImagenURL: {NuevaPieza.ImagenURL}");

           
            if (NuevaPieza == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La pieza no puede ser nula.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaPieza.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Nombre' es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaPieza.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo 'Descripción' es obligatorio.", "OK");
                return;
            }

            if (NuevaPieza.Precio <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El precio debe ser mayor a cero.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaPieza.ImagenURL) || !NuevaPieza.ImagenURL.EndsWith(".jpg") && !NuevaPieza.ImagenURL.EndsWith(".png"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese una URL de imagen válida (terminada en .jpg o .png).", "OK");
                return;
            }

            
            try
            {
                var httpClient = new System.Net.Http.HttpClient();
                var response = await httpClient.GetAsync(NuevaPieza.ImagenURL);
                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La URL de la imagen no es accesible.", "OK");
                    return;
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La URL de la imagen no es válida.", "OK");
                return;
            }

            
            PiezasList.Add(NuevaPieza);

            
            await _pedidoService.GuardarPiezasAsync(new List<Pieza>(PiezasList));
            await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza agregada exitosamente.", "OK");

           
            NuevaPieza = new Pieza();
        }

        private async Task EditarPieza(Pieza pieza)
        {
            if (pieza != null)
            {
                
                await _pedidoService.GuardarPiezasAsync(new List<Pieza>(PiezasList));
                await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza editada exitosamente.", "OK");
            }
        }

        private async Task EliminarPieza(Pieza pieza)
        {
            if (pieza != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Deseas eliminar la pieza '{pieza.Nombre}'?", "Sí", "No");
                if (confirm)
                {
                    PiezasList.Remove(pieza);
                    await _pedidoService.GuardarPiezasAsync(new List<Pieza>(PiezasList));
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza eliminada exitosamente.", "OK");
                }
            }
        }

        private async Task NavegarSiguiente()
        {

            var SistemaPagoPage = _serviceProvider.GetService<SistemaPagoPage>();
            if (SistemaPagoPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(SistemaPagoPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo navegar a SistemaPagoPage.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
