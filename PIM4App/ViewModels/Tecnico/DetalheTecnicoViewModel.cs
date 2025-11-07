using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;

namespace PIM4App.ViewModels
{
    [QueryProperty(nameof(Chamado), "Chamado")]
    public partial class DetalheTecnicoViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private Chamado _chamado;

        [ObservableProperty]
        private string _sugestaoIA;

        [ObservableProperty]
        private bool _sugestaoVisivel;

        public DetalheTecnicoViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _sugestaoVisivel = false; // Começa escondida
        }

        [RelayCommand]
        private async Task PedirAjudaIAAsync()
        {
            if (SugestaoVisivel)
            {
                SugestaoVisivel = false;
                return;
            }

            // Mostra feedback imediato
            SugestaoIA = "Analisando chamado... Aguarde.";
            SugestaoVisivel = true;

            // Busca a sugestão no serviço
            var sugestao = await _chamadoService.ObterSugestaoIAAsync(Chamado.Id);
            SugestaoIA = sugestao;
        }

        [RelayCommand]
        private async Task AssumirAsync()
        {
            if (Chamado.IdTecnicoResponsavel != null)
            {
                await Shell.Current.DisplayAlert("Aviso", "Este chamado já tem um técnico responsável.", "OK");
                return;
            }

            await _chamadoService.AssumirChamadoAsync(Chamado.Id, 99);

            Chamado.IdTecnicoResponsavel = 99;
            Chamado.Status = "Em Andamento";
            OnPropertyChanged(nameof(Chamado));

            await Shell.Current.DisplayAlert("Sucesso", "Você assumiu este chamado.", "OK");
        }

        [RelayCommand]
        private async Task FinalizarAsync()
        {
            if (Chamado.Status == "Fechado")
            {
                await Shell.Current.DisplayAlert("Aviso", "Este chamado já está fechado.", "OK");
                return;
            }

            await _chamadoService.MudarStatusChamadoAsync(Chamado.Id, "Fechado");

            Chamado.Status = "Fechado";
            OnPropertyChanged(nameof(Chamado));

            await Shell.Current.DisplayAlert("Sucesso", "Chamado finalizado.", "OK");
        }
    }
}