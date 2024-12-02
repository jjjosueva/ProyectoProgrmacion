namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer la página inicial
            MainPage = new NavigationPage(new DetallesPedidosPage());
        }
    }
}
