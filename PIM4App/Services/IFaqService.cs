using PIM4App.Models;


namespace PIM4App.Services
{
    public interface IFaqService
    {
        // Trocamos GetFaqsAsync por este
        Task<string> ObterRespostaSimuladaAsync(string pergunta);
    }
}