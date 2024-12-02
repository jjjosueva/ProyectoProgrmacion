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
            // Capturar los valores de los campos
            string pedidoEncabezado = PedidoEntry.Text;
            DateTime fechaEntrega = FechaEntregaDatePicker.Date;
            string detallesPieza = DetallesPiezaEntry.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(pedidoEncabezado) || string.IsNullOrEmpty(detallesPieza))
            {
                DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            // Mostrar los detalles en las etiquetas
            PedidoGuardadoLabel.Text = $"Encabezado del Pedido: {pedidoEncabezado}";
            FechaEntregaGuardadaLabel.Text = $"Fecha de Entrega: {fechaEntrega.ToString("yyyy-MM-dd")}";
            DetallesPiezaGuardadaLabel.Text = $"Detalles de la Pieza: {detallesPieza}";

            // Limpiar los campos de entrada
            PedidoEntry.Text = string.Empty;
            DetallesPiezaEntry.Text = string.Empty;
            FechaEntregaDatePicker.Date = DateTime.Now;

            // Mostrar un mensaje de confirmación
            DisplayAlert("Pedido Guardado", $"Pedido '{pedidoEncabezado}' guardado exitosamente.", "OK");
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            // Navegar a la siguiente página
            await Navigation.PushAsync(new PiezasPage());
        }
    }
}
