namespace ProyectoProgrmacion
{
    public partial class DetallesPedidosPage : ContentPage
    {
        private List<Pedido> pedidosList = new List<Pedido>(); // Lista para almacenar los pedidos
        private Pedido pedidoSeleccionado;

        public DetallesPedidosPage()
        {
            InitializeComponent();
            PedidosListView.ItemsSource = pedidosList;  // Vinculamos la lista al ListView
        }

        // Método para agregar un pedido
        private void OnAddPedidoClicked(object sender, EventArgs e)
        {
            string pedidoPersonalizado = PedidoPersonalizadoEntry.Text;
            string pedidoPredefinido = PedidosPicker.SelectedItem?.ToString();
            string nombreCliente = NombreClienteEntry.Text;
            string fechaEntrega = FechaEntregaDatePicker.Date.ToString("yyyy-MM-dd");
            string detallesPieza = DetallesPiezaEntry.Text;

            if (!string.IsNullOrEmpty(pedidoPersonalizado) || !string.IsNullOrEmpty(pedidoPredefinido))
            {
                var nuevoPedido = new Pedido
                {
                    PedidoNombre = !string.IsNullOrEmpty(pedidoPersonalizado) ? pedidoPersonalizado : pedidoPredefinido,
                    NombreCliente = nombreCliente,
                    FechaEntrega = fechaEntrega,
                    DetallesPieza = detallesPieza
                };

                // Agregar el nuevo pedido a la lista
                pedidosList.Add(nuevoPedido);

                // Actualizar el ListView
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                // Limpiar los campos de entrada
                PedidoPersonalizadoEntry.Text = string.Empty;
                PedidosPicker.SelectedIndex = -1;
                NombreClienteEntry.Text = string.Empty;
                FechaEntregaDatePicker.Date = DateTime.Now;
                DetallesPiezaEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Por favor ingrese o seleccione un pedido", "OK");
            }
        }

        // Método para manejar la selección de un pedido para editar
        private void OnPedidoTapped(object sender, ItemTappedEventArgs e)
        {
            pedidoSeleccionado = e.Item as Pedido;

            if (pedidoSeleccionado != null)
            {
                // Rellenar los campos con los detalles del pedido seleccionado
                PedidoPersonalizadoEntry.Text = pedidoSeleccionado.PedidoNombre;
                NombreClienteEntry.Text = pedidoSeleccionado.NombreCliente;
                FechaEntregaDatePicker.Date = DateTime.Parse(pedidoSeleccionado.FechaEntrega);
                DetallesPiezaEntry.Text = pedidoSeleccionado.DetallesPieza;
            }
        }

        // Método para editar un pedido
        private void OnEditPedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidoSeleccionado.PedidoNombre = PedidoPersonalizadoEntry.Text;
                pedidoSeleccionado.NombreCliente = NombreClienteEntry.Text;
                pedidoSeleccionado.FechaEntrega = FechaEntregaDatePicker.Date.ToString("yyyy-MM-dd");
                pedidoSeleccionado.DetallesPieza = DetallesPiezaEntry.Text;

                // Actualizamos el ListView
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                DisplayAlert("Pedido Editado", "El pedido ha sido actualizado.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pedido para editar.", "OK");
            }
        }

        // Método para eliminar un pedido
        private void OnDeletePedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidosList.Remove(pedidoSeleccionado);
                PedidosListView.ItemsSource = null;
                PedidosListView.ItemsSource = pedidosList;

                DisplayAlert("Pedido Eliminado", "El pedido ha sido eliminado correctamente.", "OK");
            }
            else
            {
                DisplayAlert("Error", "Selecciona un pedido para eliminar.", "OK");
            }
        }

        // Método para manejar la selección de un pedido predefinido
        private void OnPedidoPredefinidoSelected(object sender, EventArgs e)
        {
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
