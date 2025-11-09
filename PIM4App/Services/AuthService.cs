using PIM4App.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace PIM4App.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // A URL do seu backend
        private const string BACKEND_API_URL = "http://10.0.2.2:5043";

        private readonly JsonSerializerOptions _jsonOptions;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<LoginResponseDTO> LoginAsync(string email, string password)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiUrl = $"{BACKEND_API_URL}/api/auth/login";

                var request = new LoginRequestDTO { Email = email, Senha = password };
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, request);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>(_jsonOptions);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        // ==========================================================
                        // SALVANDO O TOKEN E O PERFIL NO "COFRE" DO CELULAR
                        // ==========================================================
                        await SecureStorage.Default.SetAsync("auth_token", loginResponse.Token);
                        await SecureStorage.Default.SetAsync("user_perfil", loginResponse.Perfil);
                        await SecureStorage.Default.SetAsync("user_nome", loginResponse.Nome);
                    }
                    return loginResponse;
                }
                return null; // Login falhou (senha errada)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no login: {ex.Message}");
                return null; // Erro de rede (API desligada)
            }
        }

        public async Task LogoutAsync()
        {
            // Limpa o "cofre"
            SecureStorage.Default.Remove("auth_token");
            SecureStorage.Default.Remove("user_perfil");
            SecureStorage.Default.Remove("user_nome");
            await Task.CompletedTask;
        }
    }
}