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

        // M�todo para cambiar a AppShell despu�s del inicio de sesi�n
        public void NavigateToAppShell()
        {
            MainPage = _serviceProvider.GetService<AppShell>();
        }
    }
}
