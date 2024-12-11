using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProyectoProgrmacion.Models; 

namespace ProyectoProgrmacion.Servicios
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5186/api/auth";

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var loginData = new User
            {
                Username = username,
                Password = password
            };

            var jsonContent = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result; 
            }

            return null;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var registerData = new UserRegisterDto
            {
                Username = username,
                Password = password
            };

            var jsonContent = JsonConvert.SerializeObject(registerData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/register", content);

            return response.IsSuccessStatusCode;
        }
    }
}
