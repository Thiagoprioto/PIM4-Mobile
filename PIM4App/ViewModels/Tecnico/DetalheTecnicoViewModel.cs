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

        [ObservableProperty]
        private ObservableCollection<InteracaoDTO> _interacoes;

        [ObservableProperty]
        private string _novoComentario;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;

        public DetalheTecnicoViewModel(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
            _interacoes = new ObservableCollection<InteracaoDTO>();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Chamado) && Chamado != null)
            {
                Task.Run(async () => await CarregarInteracoesAsync());
            }
        }

        [RelayCommand]
        private async Task CarregarInteracoesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var lista = await _chamadoService.GetInteracoesAsync(Chamado.IdChamado);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Interacoes.Clear();
                    foreach (var item in lista) Interacoes.Add(item);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AdicionarComentarioAsync()
        {
            if (string.IsNullOrWhiteSpace(NovoComentario) || IsBusy) return;

            IsBusy = true;
            try
            {
                var novaInteracao = new ComentarioDTO
                {
                    IdChamado = Chamado.IdChamado,
                    Mensagem = NovoComentario
                };

                var criada = await _chamadoService.AdicionarInteracaoAsync(novaInteracao);

                if (criada != null)
                {
                    Interacoes.Add(criada);
                    NovoComentario = string.Empty;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro", "Não foi possível enviar o comentário.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AssumirAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                await _chamadoService.AssumirChamadoAsync(Chamado.IdChamado);

                Chamado.IdStatus = 2;
                Chamado = new Chamado
                {
                    IdChamado = Chamado.IdChamado,
                    Titulo = Chamado.Titulo,
                    Descricao = Chamado.Descricao,
                    IdCategoria = Chamado.IdCategoria,
                    IdUsuarioSolicitante = Chamado.IdUsuarioSolicitante,
                    DataAbertura = Chamado.DataAbertura,
                    IdStatus = 2
                };

                await Shell.Current.DisplayAlert("Sucesso", "Você assumiu este chamado!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task FinalizarAsync()
        {
            if (IsBusy) return;

            bool confirm = await Shell.Current.DisplayAlert("Confirmar", "Deseja realmente finalizar este chamado?", "Sim", "Não");
            if (!confirm) return;

            IsBusy = true;
            try
            {
                await _chamadoService.MudarStatusParaFinalizadoAsync(Chamado.IdChamado);

                Chamado = new Chamado
                {
                    IdChamado = Chamado.IdChamado,
                    Titulo = Chamado.Titulo,
                    Descricao = Chamado.Descricao,
                    IdCategoria = Chamado.IdCategoria,
                    IdUsuarioSolicitante = Chamado.IdUsuarioSolicitante,
                    DataAbertura = Chamado.DataAbertura,
                    IdStatus = 3
                };

                await Shell.Current.DisplayAlert("Sucesso", "Chamado finalizado com sucesso.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}