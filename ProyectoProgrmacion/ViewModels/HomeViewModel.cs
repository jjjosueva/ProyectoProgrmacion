using ProyectoProgrmacion.ViewModels;
using ProyectoProgrmacion.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly IServiceProvider _serviceProvider;
    private string _welcomeMessage = "Bienvenido al Sistema de Gestión de Pedidos";
    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged();
        }
    }
    public ICommand NavegarDetallesPedidosCommand { get; }

    public HomeViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        NavegarDetallesPedidosCommand = new Command(async () => await NavegarDetallesPedidos());
    }

    private async Task NavegarDetallesPedidos()
    {
        var detallesPedidosViewModel = _serviceProvider.GetService<DetallesPedidosViewModel>();

        if (detallesPedidosViewModel != null)
        {
            var detallesPedidosPage = new DetallesPedidosPage(detallesPedidosViewModel);
            await Application.Current.MainPage.Navigation.PushAsync(detallesPedidosPage); 
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el ViewModel de detalles de pedidos.", "OK");
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
