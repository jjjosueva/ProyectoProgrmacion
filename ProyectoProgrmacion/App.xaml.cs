// ProyectoProgrmacion/App.xaml.cs
using Microsoft.Maui.Controls;

namespace ProyectoProgrmacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer el MainPage como AppShell
            MainPage = new AppShell();
        }
    }
}
