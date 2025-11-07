using PIM4App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Services
{
    public interface IChamadoService
    {
        Task<List<Chamado>> GetMeusChamadosAsync();

        // NOVO MÉTODO: Para o técnico ver tudo
        Task<List<Chamado>> GetTodosChamadosAsync();

        Task AbrirNovoChamadoAsync(Chamado novoChamado);

        Task AssumirChamadoAsync(int chamadoId, int tecnicoId);
        Task MudarStatusChamadoAsync(int chamadoId, string novoStatus);

        Task<string> ObterSugestaoIAAsync(int chamadoId);
    }
}
