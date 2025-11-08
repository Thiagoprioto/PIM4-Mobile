using PIM4App.DTO;
using PIM4App.Models;


namespace PIM4App.Services
{
    public interface IFaqService
    {
        Task<IaResponse> EnviarMensagemAsync(ChatRequest request);
    }
}