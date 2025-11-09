using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.DTO;
using PIM4App.Models;
using PIM4App.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PIM4App.ViewModels
{
    [QueryProperty(nameof(Chamado), "Chamado")]
    public partial class DetalheTecnicoViewModel : ObservableObject
    {
        private readonly IChamadoService _chamadoService;

        [ObservableProperty]
        private Chamado _chamado;

        // Propriedade para a lista de COMENTÁRIOS
        [ObservableProperty]
        private ObservableCollection<InteracaoDTO> _interacoes;

        [ObservableProperty]
        private string _novoComentario;

        [ObservableProperty]
        private bool _isBusy;

        // Removemos as propriedades de IA para limpar o código

        public DetalheTecnicoViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _interacoes = new ObservableCollection<InteracaoDTO>();
        }

        // Chamado automaticamente quando o Chamado chega na página
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(Chamado) && Chamado != null)
            {
                // Carrega os comentários quando o chamado é carregado
                Task.Run(async () => await CarregarInteracoesAsync());
            }
        }

        private bool IsNotBusy() => !IsBusy;

        // ==========================================================
        // COMANDOS DE COMENTÁRIO
        // ==========================================================

        [RelayCommand(CanExecute = nameof(IsNotBusy))]
        private async Task CarregarInteracoesAsync()
        {
            IsBusy = true;
            try
            {
                // Chama o GET /api/Interacoes/1
                var lista = await _chamadoService.GetInteracoesAsync(Chamado.IdChamado);

                Interacoes.Clear();
                foreach (var item in lista)
                {
                    Interacoes.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro Comentários", $"Falha ao carregar o histórico: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand(CanExecute = nameof(IsNotBusy))]
        private async Task AdicionarComentarioAsync()
        {
            if (string.IsNullOrWhiteSpace(NovoComentario)) return;

            IsBusy = true;
            try
            {
                var novaInteracao = new ComentarioDTO
                {
                    IdChamado = Chamado.IdChamado,
                    Mensagem = NovoComentario
                };

                // Envia para a API (POST /api/Interacoes)
                var interacaoCriada = await _chamadoService.AdicionarInteracaoAsync(novaInteracao);

                if (interacaoCriada != null)
                {
                    // Adiciona na lista visualmente imediatamente
                    Interacoes.Add(interacaoCriada);
                    NovoComentario = string.Empty; // Limpa a caixa
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro Comentários", $"Falha ao adicionar: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand(CanExecute = nameof(IsNotBusy))]
        private async Task FinalizarAsync()
        {
            if (Chamado.IdStatus == 3)
            {
                await Shell.Current.DisplayAlert("Aviso", "Este chamado já está fechado.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                await _chamadoService.MudarStatusParaFinalizadoAsync(Chamado.IdChamado);

                Chamado.IdStatus = 3;
                OnPropertyChanged(nameof(Chamado));

                await Shell.Current.DisplayAlert("Sucesso", "Chamado finalizado.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao finalizar chamado: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}