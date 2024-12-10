using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProyectoProgrmacion.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private string _welcomeMessage = "Bienvenido al Sistema de Gestión de Pedidos";
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
