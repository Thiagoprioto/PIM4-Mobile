using CommunityToolkit.Mvvm.ComponentModel;
using PIM4App.Models; // Precisa disso para saber o que é 'Chamado'

namespace PIM4App.ViewModels
{
    // ==========================================================
    // ESTE ATRIBUTO É A CHAVE DE TUDO
    // Ele diz: "Quando alguém navegar para esta página e passar um
    // parâmetro chamado 'Chamado', armazene-o na propriedade 'Chamado'"
    // ==========================================================
    [QueryProperty(nameof(Chamado), "Chamado")]
    public partial class DetalheChamadoViewModel : ObservableObject
    {
        // Esta propriedade será preenchida automaticamente pela navegação
        [ObservableProperty]
        private Chamado _chamado;

        public DetalheChamadoViewModel()
        {
        }
    }
}