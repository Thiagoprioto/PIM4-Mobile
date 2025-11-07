using CommunityToolkit.Mvvm.ComponentModel;
using PIM4App.Models;

namespace PIM4App.ViewModels
{
    [QueryProperty(nameof(Chamado), "Chamado")]
    public partial class DetalheChamadoViewModel : ObservableObject
    {
        [ObservableProperty]
        private Chamado _chamado;

        public DetalheChamadoViewModel()
        {
        }
    }
}