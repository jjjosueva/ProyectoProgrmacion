using Microsoft.Maui.Controls;
using ProyectoProgrmacion.ViewModels;
using ProyectoProgrmacion.Servicios;
using System;

namespace ProyectoProgrmacion
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var token = await _authService.LoginAsync(username, password);

            if (token != null)
            {
                await DisplayAlert("Completado", "Login exitoso", "OK");

                (Application.Current as App)?.NavigateToAppShell();
            }
            else
            {
                ErrorLabel.Text = "Nombre o Contraseña incorrectos";
                ErrorLabel.IsVisible = true;
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrar());
        }
    }
}
