using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
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
            // Adicionamos a checagem da Categoria
            if (string.IsNullOrWhiteSpace(Titulo) ||
                string.IsNullOrWhiteSpace(Descricao) ||
                string.IsNullOrWhiteSpace(CategoriaSelecionada)) // <-- ADICIONE ESTA LINHA
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preencha todos os campos, incluindo a Categoria.", "OK");
                return;
            }

            var novoChamado = new Chamado
            {
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                Categoria = this.CategoriaSelecionada // <-- ADICIONE ESTA LINHA
            };

            await _chamadoService.AbrirNovoChamadoAsync(novoChamado);

            await Shell.Current.DisplayAlert("Sucesso!", "Seu chamado foi aberto.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}