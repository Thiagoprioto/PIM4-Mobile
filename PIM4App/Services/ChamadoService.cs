using PIM4App.Models;

namespace PIM4App.Services
{
    public class ChamadoService : IChamadoService
    {
        // 'static' é VITAL aqui para a simulação funcionar entre telas diferentes
        private static List<Chamado> _listaDeChamadosSimulada = new List<Chamado>
        {
            new Chamado { Id = 1, Titulo = "Impressora não funciona", Descricao = "Sem tinta.", DataAbertura = DateTime.Now.AddDays(-2), Status = "Aberto", Categoria = "Erro Técnico" },
            new Chamado { Id = 2, Titulo = "Mouse quebrado", Descricao = "Não clica.", DataAbertura = DateTime.Now.AddDays(-1), Status = "Em Andamento", Categoria = "Problema de Hardware" },
            new Chamado { Id = 3, Titulo = "SISTEMA FORA DO AR", Descricao = "Ninguém consegue acessar o ERP.", DataAbertura = DateTime.Now, Status = "Aberto", Categoria = "Erro de Sistema" }
        };

        public async Task<List<Chamado>> GetMeusChamadosAsync()
        {
            await Task.Delay(100); // Delay menor para ser mais rápido
            return _listaDeChamadosSimulada.Take(2).ToList();
        }

        public async Task<List<Chamado>> GetTodosChamadosAsync()
        {
            await Task.Delay(100);
            // CRUCIAL: Retorna uma NOVA lista copiando os dados atuais.
            // Isso garante que quem pediu receba os dados mais recentes.
            return new List<Chamado>(_listaDeChamadosSimulada);
        }

        public async Task AbrirNovoChamadoAsync(Chamado novoChamado)
        {
            await Task.Delay(300);
            novoChamado.Id = _listaDeChamadosSimulada.Count + 1;
            novoChamado.DataAbertura = DateTime.Now;
            novoChamado.Status = "Aberto";
            _listaDeChamadosSimulada.Add(novoChamado);
        }

        public async Task AssumirChamadoAsync(int chamadoId, int tecnicoId)
        {
            // AQUI ESTÁ A MÁGICA: Atualiza a lista ORIGINAL (_listaDeChamadosSimulada)
            var chamadoReal = _listaDeChamadosSimulada.FirstOrDefault(c => c.Id == chamadoId);
            if (chamadoReal != null)
            {
                chamadoReal.IdTecnicoResponsavel = tecnicoId;
                chamadoReal.Status = "Em Andamento";
            }
            await Task.CompletedTask;
        }

        public async Task MudarStatusChamadoAsync(int chamadoId, string novoStatus)
        {
            var chamadoReal = _listaDeChamadosSimulada.FirstOrDefault(c => c.Id == chamadoId);
            if (chamadoReal != null)
            {
                chamadoReal.Status = novoStatus;
            }
            await Task.CompletedTask;
        }

        // ... dentro da classe ChamadoService

        public async Task<string> ObterSugestaoIAAsync(int chamadoId)
        {
            await Task.Delay(1000); // Simula o tempo de processamento da IA

            var chamado = _listaDeChamadosSimulada.FirstOrDefault(c => c.Id == chamadoId);
            if (chamado == null) return "Não foi possível analisar este chamado.";

            // Simulação de análise baseada na categoria
            switch (chamado.Categoria)
            {
                case "Erro Técnico":
                    return "🔍 **Análise da IA:**\nEste parece ser um problema físico. Sugiro verificar cabos, conexões de rede e se o dispositivo está ligado na tomada. Se for impressora, verifique toner e papel.";
                case "Problema de Hardware":
                    return "💻 **Análise da IA:**\nFalhas de hardware podem exigir troca de peça. Tente testar o periférico em outra máquina para confirmar se o defeito é no dispositivo ou no computador.";
                case "Erro de Sistema":
                    return "⚠️ **Análise da IA:**\nErros de sistema podem afetar múltiplos usuários. Verifique os logs do servidor e se houve alguma atualização recente que possa ter causado o problema.";
                case "Solicitação de Software":
                    return "💿 **Análise da IA:**\nVerifique se o usuário tem permissão para o software solicitado e se há licenças disponíveis.";
                default:
                    return "🤖 **Análise da IA:**\nNão tenho informações suficientes para esta categoria. Sugiro contatar o usuário para mais detalhes.";
            }
        }
    }
}