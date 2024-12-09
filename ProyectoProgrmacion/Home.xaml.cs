// ProyectoProgrmacion/Views/Home.xaml.cs
using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;

namespace ProyectoProgrmacion.Views
{
    public partial class Home : ContentPage
    {
        public Home(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
