using ProyectoProgrmacion.Servicios;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoProgrmacion
{
    public partial class Registrar : ContentPage
    {
        private readonly AuthService _authService;

        public Registrar()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var Registrar = await _authService.RegisterAsync(username, password);

            if (Registrar)
            {
                await DisplayAlert("Compleado", "Usuario regsitrado completamente", "Continuar");
                await Navigation.PopAsync(); 
            }
            else
            {
                ErrorLabel.Text = "Registro Fallido";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
