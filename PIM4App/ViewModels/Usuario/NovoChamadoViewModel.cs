using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
using PIM4App.DTO;
using System.Collections.Generic; // <-- ADICIONE ESTE

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
                IdCategoria = 1 // SIMULADO: Vamos fingir que é sempre Categoria 1
            };

            // 2. Chama o novo serviço (que usa HttpClient)
            await _chamadoService.AbrirNovoChamadoAsync(novoChamadoDto);

            await Shell.Current.DisplayAlert("Sucesso!", "Seu chamado foi aberto.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}