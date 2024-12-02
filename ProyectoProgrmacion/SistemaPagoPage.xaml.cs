namespace ProyectoProgrmacion
{
    public partial class SistemaPagoPage : ContentPage
    {
        // Variable para guardar el método de pago seleccionado
        private string metodoPagoSeleccionado;

        public SistemaPagoPage()
        {
            InitializeComponent();
        }

        private void OnMetodoPagoSelected(object sender, EventArgs e)
        {
            // Obtener el método de pago seleccionado
            metodoPagoSeleccionado = MetodoPagoPicker.SelectedItem?.ToString();
        }

        private void OnPagoClicked(object sender, EventArgs e)
        {
            // Obtener la cantidad a pagar desde el Entry
            string cantidad = PagoEntry.Text;

            // Verificar que la cantidad no esté vacía
            if (string.IsNullOrEmpty(cantidad))
            {
                DisplayAlert("Error", "Por favor ingrese una cantidad a pagar", "OK");
                return;
            }

            // Verificar que se haya seleccionado un método de pago
            if (string.IsNullOrEmpty(metodoPagoSeleccionado))
            {
                DisplayAlert("Error", "Por favor seleccione un método de pago", "OK");
                return;
            }

            // Obtener la fecha de pago seleccionada
            DateTime fechaPago = FechaPagoPicker.Date;

            // Mostrar los detalles del pago en las etiquetas
            PagoCantidadLabel.Text = $"Cantidad Pagada: {cantidad}";
            PagoFechaLabel.Text = $"Fecha de Pago: {fechaPago.ToString("yyyy-MM-dd")}";
            MetodoPagoLabel.Text = $"Método de Pago: {metodoPagoSeleccionado}";

            // Limpiar el campo de entrada para permitir nuevos pagos
            PagoEntry.Text = string.Empty;
            MetodoPagoPicker.SelectedIndex = -1;
            FechaPagoPicker.Date = DateTime.Today;

            // Mostrar un mensaje de confirmación
            DisplayAlert("Pago Realizado", $"El pago de {cantidad} ha sido realizado con éxito.\nFecha: {fechaPago.ToString("yyyy-MM-dd")}\nMétodo de pago: {metodoPagoSeleccionado}", "OK");
        }
    }
}
