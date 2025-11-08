using PIM4App.Models; // Para o ChatMessage
using PIM4App.Services;
using PIM4App.DTO; // Importa os DTOs que você criou
using System.Net.Http.Json; // Para PostAsJsonAsync e ReadFromJsonAsync
using System.Text.Json; // Para opções do JsonSerializer

namespace PIM4App.Services
{
    public class FaqService : IFaqService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // ==========================================================
        //         A URL DO SEU BACKEND (API)
        // =ANOTE O SEU NÚMERO DE PORTA CORRETO (veja no Swagger)=
        // ==========================================================
        // Usamos 10.0.2.2 em vez de localhost para o Emulador Android
        private const string BACKEND_API_URL = "http://10.0.2.2:5043"; // <<< VERIFIQUE SUA PORTA (ex: 7052)


        public FaqService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Esta é a implementação REAL
        public async Task<IaResponse> EnviarMensagemAsync(ChatRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiUrl = $"{BACKEND_API_URL}/api/ia/chat";

                // 1. Envia o 'request' (com a pergunta) para o seu Backend
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, request);

                if (response.IsSuccessStatusCode)
                {
                    // 2. Lê a 'IaResponse' (com a resposta) que o Backend enviou
                    var iaResponse = await response.Content.ReadFromJsonAsync<IaResponse>();
                    return iaResponse;
                }
                else
                {
                    // Se o backend der erro (ex: 500), retorna uma mensagem de erro
                    return new IaResponse { Resposta = "Desculpe, não consegui me conectar ao assistente. Tente mais tarde." };
                }
            }
            catch (Exception ex)
            {
                // Se o app não conseguir nem "ligar" (ex: URL errada, sem internet)
                return new IaResponse { Resposta = $"Erro de conexão: {ex.Message}" };
            }
        }
    }
}