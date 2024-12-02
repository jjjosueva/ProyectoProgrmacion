namespace ProyectoProgrmacion
{
    public partial class EncabezadoPedidosPage : ContentPage
    {
        public EncabezadoPedidosPage()
        {
            InitializeComponent();
        }

        private void OnSavePedidoClicked(object sender, EventArgs e)
        {
            // Lógica para guardar el encabezado del pedido
            string pedidoEncabezado = PedidoEntry.Text;
            DisplayAlert("Pedido Guardado", $"Pedido '{pedidoEncabezado}' guardado exitosamente.", "OK");
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            // Navegar a la siguiente página
            await Navigation.PushAsync(new PiezasPage());
        }
    }
}
