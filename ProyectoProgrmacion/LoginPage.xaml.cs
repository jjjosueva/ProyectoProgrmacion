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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorLabel.Text = "Please enter both username and password.";
                ErrorLabel.IsVisible = true;
                return;
            }

            bool isValid = await _authService.ValidarCredencialesAsync(username, password);
            if (isValid)
            {
                if (Application.Current is App app)
                {
                    app.NavigateToAppShell();
                }
            }
            else
            {
                ErrorLabel.Text = "Invalid username or password.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
