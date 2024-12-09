// C:\Users\USUARIO\Documents\PROGRAMACION 4\ProyectoProgrmacion\EncabezadoPedidosPage.xaml.cs
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class EncabezadoPedidosPage : ContentPage
    {
        public EncabezadoPedidosPage(EncabezadoPedidosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
