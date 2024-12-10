using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoProgrmacion.Services
{
    public class AuthService
    {
        private readonly Dictionary<string, string> _usuarios = new()
        {
            { "admin", "1234" },
            { "user", "password" }
        };

        public Task<bool> ValidarCredencialesAsync(string username, string password)
        {
            return Task.FromResult(_usuarios.TryGetValue(username, out var storedPassword) && storedPassword == password);
        }
    }
}
