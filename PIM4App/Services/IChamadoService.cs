using PIM4App.DTO;
using PIM4App.Models;

namespace PIM4App.Services
{
    public interface IChamadoService
    {
        // Para o Colaborador
        Task<List<Chamado>> GetMeusChamadosAsync();
        Task AbrirNovoChamadoAsync(NovoChamadoDTO novoChamado);

        // Para o Técnico
        Task<List<Chamado>> GetTodosChamadosAsync();
        Task AssumirChamadoAsync(int chamadoId);
        Task MudarStatusParaFinalizadoAsync(int chamadoId); // Nome específico
        Task<RespostaIaDTO> ObterSugestaoIAAsync(int chamadoId);
        Task<List<InteracaoDTO>> GetInteracoesAsync(int chamadoId);
        Task<InteracaoDTO> AdicionarInteracaoAsync(ComentarioDTO comentario);
    }
}