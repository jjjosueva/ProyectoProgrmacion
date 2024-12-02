namespace ProyectoProgrmacion
{
    public partial class DetallesPedidosPage : ContentPage
    {
        private List<Pedido> pedidosList = new List<Pedido>();

        public DetallesPedidosPage()
        {
            InitializeComponent();
            PedidosListView.ItemsSource = pedidosList;
        }

        // Método para agregar un pedido
        private void OnAddPedidoClicked(object sender, EventArgs e)
        {
            string pedidoPersonalizado = PedidoPersonalizadoEntry.Text;
            string pedidoPredefinido = PedidosPicker.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(pedidoPersonalizado) || !string.IsNullOrEmpty(pedidoPredefinido))
            {
                var nuevoPedido = new Pedido
                {
                    PedidoNombre = !string.IsNullOrEmpty(pedidoPersonalizado) ? pedidoPersonalizado : pedidoPredefinido,
                    FechaPedido = DateTime.Now.ToString("yyyy-MM-dd")
                };

                pedidosList.Add(nuevoPedido);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                PedidoPersonalizadoEntry.Text = string.Empty; // Limpiar el campo
                PedidosPicker.SelectedIndex = -1; // Resetear el Picker
            }
            else
            {
                DisplayAlert("Error", "Por favor ingrese o seleccione un pedido", "OK");
            }
        }

        // Método para manejar la edición de un pedido cuando se toca uno de la lista
        private void OnPedidoTapped(object sender, ItemTappedEventArgs e)
        {
            var pedidoSeleccionado = e.Item as Pedido;

            if (pedidoSeleccionado != null)
            {
                // Puedes hacer una edición de los datos seleccionados o navegar a una nueva página de edición
                DisplayAlert("Editar Pedido", $"Seleccionaste: {pedidoSeleccionado.PedidoNombre}", "OK");
            }
        }

        // Método para eliminar un pedido
        private void OnDeletePedidoClicked(object sender, EventArgs e)
        {
            if (PedidosListView.SelectedItem != null)
            {
                var pedidoSeleccionado = PedidosListView.SelectedItem as Pedido;
                pedidosList.Remove(pedidoSeleccionado);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                DisplayAlert("Pedido Eliminado", "El pedido ha sido eliminado correctamente.", "OK");
            }
        }

        // Método para manejar la selección de un pedido predefinido
        private void OnPedidoPredefinidoSelected(object sender, EventArgs e)
        {
            // Realizar cualquier acción que se necesite al seleccionar un pedido predefinido
            var picker = sender as Picker;
            var selectedPedido = picker.SelectedItem?.ToString();
            DisplayAlert("Pedido Seleccionado", $"Has seleccionado: {selectedPedido}", "OK");
        }

        // Navegar a la siguiente página (Encabezado de Pedido)
        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EncabezadoPedidosPage());
        }
    }
}
