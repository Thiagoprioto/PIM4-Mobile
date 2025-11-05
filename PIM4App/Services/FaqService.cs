using PIM4App.Models;

namespace PIM4App.Services
{
    public class FaqService : IFaqService
    {
        // Esta é a nossa nova "IA" simulada
        public async Task<string> ObterRespostaSimuladaAsync(string pergunta)
        {
            await Task.Delay(500); // Simula o processamento

            string resposta;
            pergunta = pergunta.ToLower();

            if (pergunta.Contains("impressora"))
            {
                resposta = "Entendido. Se a impressora não funciona:\n1. Verifique se está ligada.\n2. Verifique se há papel.\n3. Tente reiniciar o computador.";
            }
            else if (pergunta.Contains("senha"))
            {
                resposta = "Para resetar sua senha, use o portal Web e clique em 'Esqueci minha senha'.";
            }
            else if (pergunta.Contains("lento"))
            {
                resposta = "Se o computador está lento, tente fechar programas desnecessários e reiniciá-lo.";
            }
            else
            {
                resposta = "Desculpe, não entendi sua pergunta. Vou registrar sua dúvida para um técnico. Você também pode tentar abrir um chamado na aba 'Chamados'.";
            }

            return resposta;
        }
    }
}