namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer la p�gina de inicio como PaginaPrincipalPage
            MainPage = new NavigationPage(new PaginaPrincipalPage());
        }
    }
}
