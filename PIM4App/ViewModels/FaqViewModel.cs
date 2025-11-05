using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
using System.Collections.ObjectModel;

namespace PIM4App.ViewModels
{
    public partial class FaqViewModel : ObservableObject
    {
        private readonly IFaqService _faqService;

        [ObservableProperty]
        private ObservableCollection<ChatMessage> _mensagens;

        [ObservableProperty]
        private string _mensagemDigitada;

        public FaqViewModel(IFaqService faqService)
        {
            _faqService = faqService;
            _mensagens = new ObservableCollection<ChatMessage>();
            Mensagens.Add(new ChatMessage { Autor = "IA", Texto = "Olá! Como posso te ajudar hoje?" });
        }

        [RelayCommand]
        private async Task EnviarMensagemAsync()
        {
            // Se o texto estiver vazio, não faz nada
            if (string.IsNullOrWhiteSpace(MensagemDigitada))
                return;

            // Adiciona a pergunta do usuário
            Mensagens.Add(new ChatMessage { Autor = "Usuário", Texto = MensagemDigitada });

            var prompt = MensagemDigitada;
            MensagemDigitada = string.Empty; // Limpa o campo de texto

            // Simula a resposta da IA
            await Task.Delay(500);
            string respostaIa = await _faqService.ObterRespostaSimuladaAsync(prompt);
            Mensagens.Add(new ChatMessage { Autor = "IA", Texto = respostaIa });
        }
    }
}