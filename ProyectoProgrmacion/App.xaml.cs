namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer la p�gina inicial
            MainPage = new NavigationPage(new DetallesPedidosPage());
        }
    }
}
