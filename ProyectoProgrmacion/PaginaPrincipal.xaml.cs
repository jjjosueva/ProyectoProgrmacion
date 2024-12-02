namespace ProyectoProgrmacion
{
    public partial class PaginaPrincipalPage : ContentPage
    {
        public PaginaPrincipalPage()
        {
            InitializeComponent();
        }

        private async void OnDetallesPedidoClicked(object sender, EventArgs e)
        {
            // Navegar a la página de DetallesPedido
            await Navigation.PushAsync(new DetallesPedidosPage());
        }

        private async void OnEncabezadoPedidoClicked(object sender, EventArgs e)
        {
            // Navegar a la página de EncabezadoPedido
            await Navigation.PushAsync(new EncabezadoPedidosPage());
        }

        private async void OnPiezasDisponiblesClicked(object sender, EventArgs e)
        {
            // Navegar a la página de PiezasDisponibles
            await Navigation.PushAsync(new PiezasPage());
        }

        private async void OnSistemaPagoClicked(object sender, EventArgs e)
        {
            // Navegar a la página de SistemaPago
            await Navigation.PushAsync(new SistemaPagoPage());
        }
    }
}
