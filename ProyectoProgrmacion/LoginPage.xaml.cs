using Microsoft.Maui.Controls;
using ProyectoProgrmacion.Services;

namespace ProyectoProgrmacion.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;

        public LoginPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (username == "TrAndroid" && password == "12345")
            {
                await DisplayAlert("Inicio de sesi�n", "�Inicio de sesi�n exitoso!", "OK");
                await Navigation.PushAsync(new AppShell());
            }
            else
            {
                await DisplayAlert("Inicio de sesi�n fallido", "Usuario o contrase�a incorrectos.", "OK");
            }
        }
    }
}
