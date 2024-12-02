namespace ProyectoProgrmacion
{
    public partial class DetallesPedidosPage : ContentPage
    {
        public DetallesPedidosPage()
        {
            InitializeComponent();
        }

        // Este método se ejecuta cuando se selecciona un pedido predefinido.
        private void OnPedidoPredefinidoSelected(object sender, EventArgs e)
        {
            // Obtener el índice seleccionado
            var picker = sender as Picker;
            var selectedPedido = picker.SelectedItem.ToString();

            // Mostrar un mensaje o realizar alguna acción con el pedido seleccionado
            DisplayAlert("Pedido Seleccionado", $"Has seleccionado: {selectedPedido}", "OK");
        }

        // Guardar pedido: Puede ser personalizado o seleccionado de la lista.
        private void OnSavePedidoClicked(object sender, EventArgs e)
        {
            string pedidoPersonalizado = PedidoPersonalizadoEntry.Text;
            string pedidoPredefinido = PedidosPicker.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(pedidoPersonalizado))
            {
                // Guardar el pedido personalizado
                DisplayAlert("Pedido Guardado", $"Pedido personalizado: {pedidoPersonalizado}", "OK");
            }
            else if (!string.IsNullOrEmpty(pedidoPredefinido))
            {
                // Guardar el pedido predefinido
                DisplayAlert("Pedido Guardado", $"Pedido predefinido: {pedidoPredefinido}", "OK");
            }
            else
            {
                DisplayAlert("Error", "Por favor ingrese o seleccione un pedido", "OK");
            }
        }

        // Navegar a la siguiente página (Encabezado de Pedido)
        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EncabezadoPedidosPage());
        }
    }
}
