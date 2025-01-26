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
        private readonly PiezaService _piezaService;
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<Pieza> PiezasList { get; set; } = new ObservableCollection<Pieza>();

        private Pieza _nuevaPieza = new Pieza();
        public Pieza NuevaPieza
        {
            get => _nuevaPieza;
            set
            {
                _nuevaPieza = value;
                OnPropertyChanged();
            }
        }

        public Command AgregarPiezaCommand { get; }
        public Command<Pieza> EditarPiezaCommand { get; }
        public Command<Pieza> EliminarPiezaCommand { get; }
        public Command NavegarSiguienteCommand { get; }

        public PiezasViewModel(PiezaService piezaService, IServiceProvider serviceProvider)
        {
            _piezaService = piezaService;
            _serviceProvider = serviceProvider;

            AgregarPiezaCommand = new Command(async () => await AgregarPieza());
            EditarPiezaCommand = new Command<Pieza>(async (pieza) => await EditarPieza(pieza));
            EliminarPiezaCommand = new Command<Pieza>(async (pieza) => await EliminarPieza(pieza));
            NavegarSiguienteCommand = new Command(async () => await NavegarSiguiente());

            CargarPiezas();
        }

        private async void CargarPiezas()
        {
            var lista = await _piezaService.ObtenerPiezasAsync();
            PiezasList.Clear();
            foreach (var pieza in lista)
            {
                PiezasList.Add(pieza);
            }
        }

        private async Task AgregarPieza()
        {
            if (string.IsNullOrWhiteSpace(NuevaPieza.Nombre) ||
                string.IsNullOrWhiteSpace(NuevaPieza.Descripcion) ||
                NuevaPieza.Precio <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            await _piezaService.AgregarPiezaAsync(NuevaPieza);
            PiezasList.Add(NuevaPieza);
            NuevaPieza = new Pieza();

            await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza agregada", "OK");
        }

        private async Task EditarPieza(Pieza pieza)
        {
            if (pieza != null)
            {
                await _piezaService.EditarPiezaAsync(pieza);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza editada", "OK");
            }
        }

        private async Task EliminarPieza(Pieza pieza)
        {
            if (pieza != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Eliminar '{pieza.Nombre}'?", "Sí", "No");
                if (confirm)
                {
                    await _piezaService.EliminarPiezaAsync(pieza);
                    PiezasList.Remove(pieza);
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza eliminada", "OK");
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
