using PIM4App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Services
{
    // A classe "assina" o contrato da interface
    public class ChamadoService : IChamadoService
    {
        // SIMULAÇÃO DE BANCO DE DADOS
        // No app real, esta lista estaria vazia e você buscaria
        // os dados da sua API ASP.NET
        private static List<Chamado> _listaDeChamadosSimulada = new List<Chamado>
        {

        };


        // Implementação do contrato para buscar a lista
        public async Task<List<Chamado>> GetMeusChamadosAsync()
        {
            // Simula uma espera da rede
            await Task.Delay(500);

            // Retorna a lista simulada
            return _listaDeChamadosSimulada;
        }

        // Implementação do contrato para abrir um novo chamado
        public async Task AbrirNovoChamadoAsync(Chamado novoChamado)
        {
            // Simula uma espera da rede
            await Task.Delay(500);

            // Simula a adição no "banco de dados"
            novoChamado.Id = _listaDeChamadosSimulada.Count + 1; // Simula um ID novo
            novoChamado.DataAbertura = DateTime.Now;
            novoChamado.Status = "Aberto";

            _listaDeChamadosSimulada.Add(novoChamado);
        }
    }
}
