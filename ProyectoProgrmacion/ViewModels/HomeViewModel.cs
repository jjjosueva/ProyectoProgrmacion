// ProyectoProgrmacion/ViewModels/HomeViewModel.cs
using ProyectoProgrmacion.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace ProyectoProgrmacion.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        // Propiedad de bienvenida
        private string welcomeMessage;
        public string WelcomeMessage
        {
            get => welcomeMessage;
            set
            {
                welcomeMessage = value;
                OnPropertyChanged();
            }
        }

        // Comandos
        public Command NavigateToPedidosCommand { get; }

        private readonly IServiceProvider _serviceProvider;

        public HomeViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            WelcomeMessage = "Bienvenido al Sistema de Gestión de Pedidos";

            NavigateToPedidosCommand = new Command(async () => await NavigateToPedidos());
        }

        private async Task NavigateToPedidos()
        {
            var detallesPedidosPage = _serviceProvider.GetService<DetallesPedidosPage>();
            if (detallesPedidosPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(detallesPedidosPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo navegar a DetallesPedidosPage.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
