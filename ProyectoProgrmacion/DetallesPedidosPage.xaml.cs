namespace ProyectoProgrmacion
{
    public partial class DetallesPedidosPage : ContentPage
    {
        private List<Pedido> pedidosList = new List<Pedido>();
        private Pedido pedidoSeleccionado;

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

        // Método para manejar la selección de un pedido para editar
        private void OnPedidoTapped(object sender, ItemTappedEventArgs e)
        {
            pedidoSeleccionado = e.Item as Pedido;

            if (pedidoSeleccionado != null)
            {
                PedidoPersonalizadoEntry.Text = pedidoSeleccionado.PedidoNombre; // Rellenar el Entry con el pedido seleccionado
            }
        }

        // Método para editar un pedido
        private void OnEditPedidoClicked(object sender, EventArgs e)
        {
            if (pedidoSeleccionado != null)
            {
                pedidoSeleccionado.PedidoNombre = PedidoPersonalizadoEntry.Text;
                pedidoSeleccionado.FechaPedido = DateTime.Now.ToString("yyyy-MM-dd"); // Se actualiza la fecha al editar

                // Actualizamos la lista
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
