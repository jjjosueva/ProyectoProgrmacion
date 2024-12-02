namespace ProyectoProgrmacion
{
    public partial class PiezasPage : ContentPage
    {
        public PiezasPage()
        {
            InitializeComponent();
            // Cargar datos de ejemplo para las piezas
            PiezasListView.ItemsSource = new List<Pieza>
            {
                new Pieza { NombrePieza = "Pieza A", Precio = "$50" },
                new Pieza { NombrePieza = "Pieza B", Precio = "$30" },
            };
        }

        private async void OnNextPageClicked(object sender, EventArgs e)
        {
            // Navegar a la siguiente página
            await Navigation.PushAsync(new SistemaPagoPage());
        }
    }
}
