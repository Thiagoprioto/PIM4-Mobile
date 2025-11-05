using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class ChamadosPage : ContentPage
{
    public ChamadosPage(ChamadosViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Pega a ViewModel e executa o comando de carregar
        if (BindingContext is ChamadosViewModel viewModel)
        {
            // Pede para a ViewModel carregar os chamados
            await viewModel.CarregarChamadosCommand.ExecuteAsync(null);
        }
    }
}