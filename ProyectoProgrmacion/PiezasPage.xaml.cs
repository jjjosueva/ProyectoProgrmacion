namespace ProyectoProgrmacion
{
    public partial class PiezasPage : ContentPage
    {
        public PiezasPage()
        {
            InitializeComponent();
        }

        private void OnSavePiezaClicked(object sender, EventArgs e)
        {
            // Obtener los datos ingresados en los campos
            string nombrePieza = NombrePiezaEntry.Text;
            string precioPieza = PrecioPiezaEntry.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombrePieza) || string.IsNullOrEmpty(precioPieza))
            {
                DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            // Mostrar los detalles ingresados en las etiquetas correspondientes
            NombrePiezaGuardadaLabel.Text = $"Nombre de la Pieza: {nombrePieza}";
            PrecioPiezaGuardadaLabel.Text = $"Precio: {precioPieza}";

            // Limpiar los campos para un nuevo ingreso
            NombrePiezaEntry.Text = string.Empty;
            PrecioPiezaEntry.Text = string.Empty;

            // Mostrar un mensaje de confirmación
            DisplayAlert("Pieza Guardada", $"Pieza '{nombrePieza}' guardada exitosamente.", "OK");
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            // Navegar a la siguiente página
            await Navigation.PushAsync(new SistemaPagoPage());
        }
    }
}
