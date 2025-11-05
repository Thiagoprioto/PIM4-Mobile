using PIM4App.ViewModels;
namespace PIM4App.Views;

public partial class FaqPage : ContentPage
{
    public FaqPage(FaqViewModel viewModel)
    {
        InitializeComponent();

        // ESTA LINHA É A MAIS IMPORTANTE DO APP AGORA
        // Ela conecta a tela (View) com a lógica (ViewModel)
        BindingContext = viewModel;
    }
}