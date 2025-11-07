using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class TecnicoDashboardPage : ContentPage
{
    public TecnicoDashboardPage(TecnicoDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // Este método roda SEMPRE que a tela aparece (incluindo quando você volta)
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TecnicoDashboardViewModel viewModel)
        {
            // Força o recarregamento
            await viewModel.CarregarTodosChamadosCommand.ExecuteAsync(null);
        }
    }
}