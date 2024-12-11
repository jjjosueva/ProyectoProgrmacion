using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class SistemaPagoPage : ContentPage
    {
        public SistemaPagoPage(SistemaPagoViewModel viewModel)
        {
            InitializeComponent();

            // Asignar el ViewModel como BindingContext
            BindingContext = viewModel;

            System.Diagnostics.Debug.WriteLine("SistemaPagoPage inicializada correctamente.");
        }
    }
}
