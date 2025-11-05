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
        // Contrato para buscar a lista de chamados do usuário
        Task<List<Chamado>> GetMeusChamadosAsync();

        // Contrato para abrir um novo chamado
        Task AbrirNovoChamadoAsync(Chamado novoChamado);
    }
}
