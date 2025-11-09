using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Models; // (Adicione este using, se estiver faltando)
using PIM4App.Services;
using PIM4App.Views;
using System;
using System.Collections.ObjectModel;

namespace PIM4App.ViewModels
{
    public partial class ChamadosViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private ObservableCollection<Chamado> _chamados;

        // ==========================================================
        // 1. ADICIONE A PROPRIEDADE 'IsBusy' QUE FALTAVA
        // ==========================================================
        [ObservableProperty]
        private bool _isBusy;

        public ChamadosViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _chamados = new ObservableCollection<Chamado>();
        }

        // ==========================================================
        // 2. MÉTODO 'CarregarChamadosAsync' COMPLETO
        // ==========================================================
        [RelayCommand]
        private async Task CarregarChamadosAsync()
        {
            IsBusy = true; // Inicia o "Carregando"
            try
            {
                // Chama o método correto do serviço de usuário
                var listaAtualizada = await _chamadoService.GetMeusChamadosAsync();

                Chamados.Clear();
                foreach (var chamado in listaAtualizada)
                {
                    Chamados.Add(chamado);
                }
            }
            catch (Exception ex)
            {
                // Mostra um erro se a API falhar
                await Shell.Current.DisplayAlert("Erro de Conexão", $"Falha ao carregar seus chamados: {ex.Message}", "OK");
            }
            finally
            {
                // Garante que o "Carregando" termine, mesmo se der erro
                IsBusy = false;
            }
        }

        // ==========================================================
        // (Aqui ficam seus outros comandos, como 'IrParaNovoChamadoAsync', etc.)
        // ==========================================================
        [RelayCommand]
        private async Task IrParaNovoChamadoAsync()
        {
            await Shell.Current.GoToAsync(nameof(NovoChamadoPage));
        }

        [RelayCommand]
        private async Task IrParaFaqAsync()
        {
            await Shell.Current.GoToAsync(nameof(FaqPage));
        }

        [RelayCommand]
        private async Task SelecionarChamadoAsync(Chamado chamado)
        {
            if (chamado == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetalheChamadoPage), new Dictionary<string, object>
            {
                { "Chamado", chamado }
            });
        }
    }
}