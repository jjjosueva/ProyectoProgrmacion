namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer la página de inicio como PaginaPrincipalPage
            MainPage = new NavigationPage(new PaginaPrincipalPage());
        }
    }
}
