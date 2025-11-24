using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
using PIM4App.DTO;
using System.Collections.Generic; // <-- Verifique se este using está presente

namespace PIM4App.ViewModels
{
    public partial class NovoChamadoViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private string _titulo;

        [ObservableProperty]
        private string _descricao;

        // Propriedades para o Picker (Dropdown) de Categoria
        public List<string> Categorias { get; }

        [ObservableProperty]
        private string _categoriaSelecionada;

        // ==========================================================
        // 1. ADIÇÃO DAS PROPRIEDADES DE PRIORIDADE
        // ==========================================================
        public List<string> Prioridades { get; }

        [ObservableProperty]
        private string _prioridadeSelecionada;
        // ==========================================================


        public NovoChamadoViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;

            // Inicializa a lista de categorias que o usuário pode escolher
            Categorias = new List<string>
            {
                "Erro Técnico",
                "Erro de Sistema",
                "Problema de Hardware",
                "Dúvida",
                "Solicitação de Software"
            };
            // Define um valor padrão
            CategoriaSelecionada = Categorias[0];


            // ==========================================================
            // 2. INICIALIZAÇÃO DA LISTA DE PRIORIDADES
            // ==========================================================
            Prioridades = new List<string> { "Baixa", "Média", "Alta", "Urgente" };
            PrioridadeSelecionada = "Baixa"; // Define "Baixa" como padrão
        }

        [RelayCommand]
        private async Task SalvarAsync()
        {
            if (string.IsNullOrWhiteSpace(Titulo) ||
                string.IsNullOrWhiteSpace(Descricao))
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preencha o Título e a Descrição.", "OK");
                return;
            }

            // 1. Cria o DTO que o Backend espera
            var novoChamadoDto = new PIM4App.DTO.NovoChamadoDTO
            {
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                IdCategoria = 1, // SIMULADO: (Troque isso pela lógica do Picker de Categoria quando o tiver)

                // ==========================================================
                // 3. ENVIO DA PRIORIDADE SELECIONADA
                // ==========================================================
                Prioridade = this.PrioridadeSelecionada
            };

            // 2. Chama o serviço (que usa HttpClient)
            await _chamadoService.AbrirNovoChamadoAsync(novoChamadoDto);

            await Shell.Current.DisplayAlert("Sucesso!", "Seu chamado foi aberto.", "OK");

            // Limpa os campos para um novo chamado
            Titulo = string.Empty;
            Descricao = string.Empty;
            CategoriaSelecionada = Categorias[0];
            PrioridadeSelecionada = Prioridades[0];

            // Volta para a página anterior (lista de chamados)
            await Shell.Current.GoToAsync("..");
        }
    }
}