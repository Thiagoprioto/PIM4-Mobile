using PIM4App.ViewModels;

namespace PIM4App.Views;

public partial class DetalheChamadoPage : ContentPage
{
    public DetalheChamadoPage(DetalheChamadoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}