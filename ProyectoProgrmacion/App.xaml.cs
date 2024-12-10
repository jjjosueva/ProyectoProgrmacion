using Microsoft.Maui.Controls;

namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // Mostrar LoginPage al inicio
            MainPage = new NavigationPage(_serviceProvider.GetService<Views.LoginPage>());
        }

        // Método para cambiar a AppShell después del inicio de sesión
        public void NavigateToAppShell()
        {
            MainPage = _serviceProvider.GetService<AppShell>();
        }
    }
}
