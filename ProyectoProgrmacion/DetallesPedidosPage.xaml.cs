// ProyectoProgrmacion/Views/DetallesPedidosPage.xaml.cs
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class DetallesPedidosPage : ContentPage
    {
        public DetallesPedidosPage(DetallesPedidosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
