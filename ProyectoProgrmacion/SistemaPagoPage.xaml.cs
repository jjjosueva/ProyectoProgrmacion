namespace ProyectoProgrmacion
{
    public partial class SistemaPagoPage : ContentPage
    {
        public SistemaPagoPage()
        {
            InitializeComponent();
        }

        private void OnPagoClicked(object sender, EventArgs e)
        {
            // L�gica para realizar el pago
            string cantidad = PagoEntry.Text;
            DisplayAlert("Pago Realizado", $"El pago de {cantidad} ha sido realizado con �xito.", "OK");
        }
    }
}
