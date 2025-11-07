using PIM4App.Models;


namespace PIM4App.Services
{
    public interface IFaqService
    {
        Task<string> ObterRespostaSimuladaAsync(string pergunta);
    }
}