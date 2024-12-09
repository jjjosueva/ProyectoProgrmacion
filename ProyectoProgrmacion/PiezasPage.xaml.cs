// C:\Users\USUARIO\Documents\PROGRAMACION 4\ProyectoProgrmacion\PiezasPage.xaml.cs
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class PiezasPage : ContentPage
    {
        public PiezasPage(PiezasViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
