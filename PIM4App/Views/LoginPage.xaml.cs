using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class LoginPage : ContentPage
{
    // Pede a LoginViewModel no construtor
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();

        // Conecta a View (visual) com a ViewModel (lógica)
        BindingContext = viewModel;
    }
}