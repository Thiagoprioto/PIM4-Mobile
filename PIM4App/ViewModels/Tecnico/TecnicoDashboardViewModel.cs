using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Services;
using PIM4App.Views;
using System.Collections.ObjectModel;
using System;

namespace PIM4App.ViewModels
{

    public partial class TecnicoDashboardViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private ObservableCollection<Chamado> _todosChamados;

        [ObservableProperty]
        private bool _isBusy;

        public TecnicoDashboardViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _todosChamados = new ObservableCollection<Chamado>();
        }

        [RelayCommand]
        private async Task CarregarTodosChamadosAsync()
        {
            IsBusy = true;
            try
            {
                var listaAtualizada = await _chamadoService.GetTodosChamadosAsync();

                TodosChamados.Clear();
                foreach (var chamado in listaAtualizada)
                {
                    TodosChamados.Add(chamado);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao carregar todos os chamados: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
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