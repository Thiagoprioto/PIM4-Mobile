using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class NovoChamadoPage : ContentPage
{
    public NovoChamadoPage(NovoChamadoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}