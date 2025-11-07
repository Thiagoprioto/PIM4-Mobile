using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models;
using PIM4App.Views;
using PIM4App.Services;
using System.Collections.ObjectModel;

namespace PIM4App.ViewModels
{
    public partial class ChamadosViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private ObservableCollection<Chamado> _chamados;

        public ChamadosViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _chamados = new ObservableCollection<Chamado>();
        }

        [RelayCommand]
        private async Task CarregarChamadosAsync()
        {
            var lista = await _chamadoService.GetMeusChamadosAsync();

            Chamados.Clear();
            foreach (var chamado in lista)
            {
                Chamados.Add(chamado);
            }
        }

        [RelayCommand]
        private async Task IrParaNovoChamadoAsync()
        {
            await Shell.Current.GoToAsync(nameof(NovoChamadoPage));
        }

        [RelayCommand]
        private async Task SelecionarChamadoAsync(Chamado chamado)
        {
            // Se o chamado for nulo, não faz nada
            if (chamado == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetalheChamadoPage), new Dictionary<string, object>
            {
                { "Chamado", chamado }
            });
        }
    }
}