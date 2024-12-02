namespace ProyectoProgrmacion
{
    public partial class SistemaPagoPage : ContentPage
    {
        // Variable para guardar el m�todo de pago seleccionado
        private string metodoPagoSeleccionado;

        public SistemaPagoPage()
        {
            InitializeComponent();
        }

        private void OnMetodoPagoSelected(object sender, EventArgs e)
        {
            // Obtener el m�todo de pago seleccionado
            metodoPagoSeleccionado = MetodoPagoPicker.SelectedItem?.ToString();
        }

        private void OnPagoClicked(object sender, EventArgs e)
        {
            // Obtener la cantidad a pagar desde el Entry
            string cantidad = PagoEntry.Text;

            // Verificar que la cantidad no est� vac�a
            if (string.IsNullOrEmpty(cantidad))
            {
                DisplayAlert("Error", "Por favor ingrese una cantidad a pagar", "OK");
                return;
            }

            // Verificar que se haya seleccionado un m�todo de pago
            if (string.IsNullOrEmpty(metodoPagoSeleccionado))
            {
                DisplayAlert("Error", "Por favor seleccione un m�todo de pago", "OK");
                return;
            }

            // Obtener la fecha de pago seleccionada
            DateTime fechaPago = FechaPagoPicker.Date;

            // Mostrar los detalles del pago en las etiquetas
            PagoCantidadLabel.Text = $"Cantidad Pagada: {cantidad}";
            PagoFechaLabel.Text = $"Fecha de Pago: {fechaPago.ToString("yyyy-MM-dd")}";
            MetodoPagoLabel.Text = $"M�todo de Pago: {metodoPagoSeleccionado}";

            // Limpiar el campo de entrada para permitir nuevos pagos
            PagoEntry.Text = string.Empty;
            MetodoPagoPicker.SelectedIndex = -1;
            FechaPagoPicker.Date = DateTime.Today;

            // Mostrar un mensaje de confirmaci�n
            DisplayAlert("Pago Realizado", $"El pago de {cantidad} ha sido realizado con �xito.\nFecha: {fechaPago.ToString("yyyy-MM-dd")}\nM�todo de pago: {metodoPagoSeleccionado}", "OK");
        }
    }
}
