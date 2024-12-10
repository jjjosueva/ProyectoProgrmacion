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


            MainPage = new NavigationPage(_serviceProvider.GetService<Views.LoginPage>());
        }

        
        public void NavigateToAppShell()
        {
            MainPage = _serviceProvider.GetService<AppShell>();
        }
    }
}
