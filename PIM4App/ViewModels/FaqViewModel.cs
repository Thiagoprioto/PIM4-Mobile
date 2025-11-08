using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models; // Para o ChatMessage
using PIM4App.DTO;   // Para os DTOs
using PIM4App.Services;
using System.Collections.ObjectModel;
using System; // Para o Guid

namespace PIM4App.ViewModels
{
    public partial class FaqViewModel : ObservableObject
    {
        private readonly IFaqService _faqService;
        private readonly string _sessionId; // <-- VAI GUARDAR O ID DA SESSÃO

        [ObservableProperty]
        private ObservableCollection<ChatMessage> _mensagens;

        [ObservableProperty]
        private string _mensagemDigitada;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarMensagemCommand))]
        private bool _estaCarregando;

        public FaqViewModel(IFaqService faqService)
        {
            _faqService = faqService;
            _mensagens = new ObservableCollection<ChatMessage>();
            Mensagens.Add(new ChatMessage { Autor = "IA", Texto = "Olá! Como posso te ajudar hoje?" });

            // CRIA UM ID ÚNICO PARA ESTA SESSÃO DE CHAT
            _sessionId = Guid.NewGuid().ToString();
        }

        [RelayCommand(CanExecute = nameof(NaoEstaCarregando))]
        private async Task EnviarMensagemAsync()
        {
            if (string.IsNullOrWhiteSpace(MensagemDigitada))
                return;

            EstaCarregando = true; // Desativa o botão
            var pergunta = MensagemDigitada;
            MensagemDigitada = string.Empty;

            try
            {
                // 1. Adiciona a pergunta do usuário
                Mensagens.Add(new ChatMessage { Autor = "Usuário", Texto = pergunta });

                // 2. Prepara o DTO (AGORA COM O SESSION ID)
                var request = new ChatRequest
                {
                    Pergunta = pergunta,
                    SessionId = _sessionId  // <-- CORREÇÃO IMPORTANTE
                };

                // 3. CHAMA O SERVIÇO REAL
                var iaResponse = await _faqService.EnviarMensagemAsync(request);

                // 4. Adiciona a resposta (com checagem de erro)
                if (iaResponse != null && !string.IsNullOrEmpty(iaResponse.Resposta))
                {
                    Mensagens.Add(new ChatMessage { Autor = "IA", Texto = iaResponse.Resposta });
                }
                else
                {
                    Mensagens.Add(new ChatMessage { Autor = "IA", Texto = "Desculpe, tive um problema para processar sua resposta." });
                }
            }
            catch (Exception ex)
            {
                // ESTA É A CORREÇÃO DA MENSAGEM (A VOZ DA GABRIELLY)
                Mensagens.Add(new ChatMessage
                {
                    Autor = "IA",
                    Texto = "Ai, desculpe! 😥 Estou demorando um pouquinho mais que o normal para responder. Você pode tentar enviar sua pergunta de novo?"
                });
            }
            finally
            {
                // GARANTE QUE O BOTÃO SEJA REATIVADO, NÃO IMPORTA O QUE ACONTEÇA
                EstaCarregando = false;
            }
        }

        private bool NaoEstaCarregando()
        {
            return !EstaCarregando;
        }
    }
}