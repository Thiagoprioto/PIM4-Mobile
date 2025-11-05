using CommunityToolkit.Mvvm.Input;
using PIM4App.Views; // Precisamos disso para saber a rota da LoginPage

namespace PIM4App.ViewModels
{
    public partial class PerfilViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public PerfilViewModel()
        {
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            // (Futuro) Aqui você limparia qualquer dado de sessão
            // ou Token de usuário que estivesse salvo.

            // Navega de volta para a LoginPage.
            // Usamos "//" (navegação absoluta) para garantir
            // que estamos resetando a pilha de navegação.
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}