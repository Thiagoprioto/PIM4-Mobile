using PIM4App.DTO;
using PIM4App.Models;
using System.Net.Http.Headers; // Para o Token (Bearer)
using System.Net.Http.Json;
using System.Text.Json;
using CommunityToolkit.Maui.Storage; // Para o SecureStorage

namespace PIM4App.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string BACKEND_API_URL = "http://10.0.2.2:5043";
        private readonly JsonSerializerOptions _jsonOptions;

        public ChamadoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // ==========================================================
        // FUNÇÃO "MÁGICA": Cria um HttpClient e adiciona o Token
        // ==========================================================
        private async Task<HttpClient> CreateAuthenticatedClientAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // Pega o Token que o AuthService salvou no "cofre"
            string token = await SecureStorage.Default.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                // Se não houver token, lança um erro (o app deve voltar ao login)
                throw new Exception("Token de autenticação não encontrado. Faça login novamente.");
            }

            // Adiciona o Token ao cabeçalho (Header) da requisição
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        // ==========================================================
        // MÉTODOS REAIS (COLABORADOR)
        // ==========================================================
        public async Task<List<Chamado>> GetMeusChamadosAsync()
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados/meus";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var chamados = await response.Content.ReadFromJsonAsync<List<Chamado>>(_jsonOptions);
                    return chamados ?? new List<Chamado>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar MEUS chamados: {ex.Message}");
            }
            return new List<Chamado>();
        }

        public async Task AbrirNovoChamadoAsync(NovoChamadoDTO novoChamado)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados";
                var response = await client.PostAsJsonAsync(apiUrl, novoChamado);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar chamado: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar chamado: {ex.Message}");
            }
        }

        // ==========================================================
        // MÉTODOS REAIS (TÉCNICO)
        // ==========================================================
        public async Task<List<Chamado>> GetTodosChamadosAsync()
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados/todos";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var chamados = await response.Content.ReadFromJsonAsync<List<Chamado>>(_jsonOptions);
                    return chamados ?? new List<Chamado>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar TODOS chamados: {ex.Message}");
            }
            return new List<Chamado>();
        }

        public async Task AssumirChamadoAsync(int chamadoId)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados/{chamadoId}/assumir";
                var response = await client.PutAsync(apiUrl, null); // PUT sem corpo

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao assumir chamado: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao assumir chamado: {ex.Message}");
            }
        }

        public async Task MudarStatusParaFinalizadoAsync(int chamadoId)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados/{chamadoId}/finalizar";
                var response = await client.PutAsync(apiUrl, null);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao finalizar chamado: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao finalizar chamado: {ex.Message}");
            }
        }

        public async Task<RespostaIaDTO> ObterSugestaoIAAsync(int chamadoId)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                string apiUrl = $"{BACKEND_API_URL}/api/Chamados/{chamadoId}/sugestaoia";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RespostaIaDTO>(_jsonOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sugestão IA: {ex.Message}");
            }
            return null; // Retorna nulo se der erro
        }
        public async Task<List<InteracaoDTO>> GetInteracoesAsync(int chamadoId)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                // Chama o novo endpoint GET /api/Interacoes/1
                string apiUrl = $"{BACKEND_API_URL}/api/Interacoes/{chamadoId}";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var interacoes = await response.Content.ReadFromJsonAsync<List<InteracaoDTO>>(_jsonOptions);
                    return interacoes ?? new List<InteracaoDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar interações: {ex.Message}");
            }
            return new List<InteracaoDTO>(); // Retorna lista vazia se der erro
        }

        public async Task<InteracaoDTO> AdicionarInteracaoAsync(ComentarioDTO comentario)
        {
            try
            {
                var client = await CreateAuthenticatedClientAsync();
                // Chama o novo endpoint POST /api/Interacoes
                string apiUrl = $"{BACKEND_API_URL}/api/Interacoes";

                var response = await client.PostAsJsonAsync(apiUrl, comentario);

                if (response.IsSuccessStatusCode)
                {
                    // Retorna o comentário que acabou de ser criado
                    var interacaoCriada = await response.Content.ReadFromJsonAsync<InteracaoDTO>(_jsonOptions);
                    return interacaoCriada;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar interação: {ex.Message}");
            }
            return null; // Retorna nulo se der erro
        }
    }
}