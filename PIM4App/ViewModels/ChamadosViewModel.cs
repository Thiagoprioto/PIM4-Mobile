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

            // CORREÇÃO: Removemos o carregamento daqui.
            // A tela (View) vai chamar o comando "Carregar" agora.
        }

        [RelayCommand]
        private async Task CarregarChamadosAsync()
        {
            // Busca os dados do nosso serviço simulado
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

            // Navega para a página de Detalhe, passando o objeto 'chamado'
            // como um parâmetro de navegação.
            // O [QueryProperty] que criamos na DetalheChamadoViewModel
            // vai "capturar" este objeto.
            await Shell.Current.GoToAsync(nameof(DetalheChamadoPage), new Dictionary<string, object>
            {
                { "Chamado", chamado }
            });
        }
    }
}