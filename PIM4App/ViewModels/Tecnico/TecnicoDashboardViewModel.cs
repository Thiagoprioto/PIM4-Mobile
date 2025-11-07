using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
using PIM4App.Views;
using System.Collections.ObjectModel;

namespace PIM4App.ViewModels
{
    public partial class TecnicoDashboardViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private ObservableCollection<Chamado> _todosChamados;

        public TecnicoDashboardViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _todosChamados = new ObservableCollection<Chamado>();
            // Removi o carregamento inicial daqui, pois o OnAppearing já vai fazer isso.
        }

        [RelayCommand]
        private async Task CarregarTodosChamadosAsync()
        {
            var listaAtualizada = await _chamadoService.GetTodosChamadosAsync();

            TodosChamados = new ObservableCollection<Chamado>(listaAtualizada);
        }

        [RelayCommand]
        private async Task SelecionarChamadoAsync(Chamado chamado)
        {
            if (chamado == null) return;
            await Shell.Current.GoToAsync(nameof(DetalheTecnicoPage), new Dictionary<string, object>
            {
                { "Chamado", chamado }
            });
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}