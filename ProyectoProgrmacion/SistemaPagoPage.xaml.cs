// C:\Users\USUARIO\Documents\PROGRAMACION 4\ProyectoProgrmacion\SistemaPagoPage.xaml.cs
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class SistemaPagoPage : ContentPage
    {
        public SistemaPagoPage(SistemaPagoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
