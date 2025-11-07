using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class DetalheTecnicoPage : ContentPage
{
    public DetalheTecnicoPage(DetalheTecnicoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}