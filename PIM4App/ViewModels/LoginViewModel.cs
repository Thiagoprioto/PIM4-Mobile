using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Services;
using PIM4App.Views; // Necessário para saber o nome das páginas de destino

namespace PIM4App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preencha o usuário e a senha.", "OK");
                return;
            }

            try
            {
                string perfilUsuario = await _authService.LoginAsync(Username, Password);

                if (perfilUsuario != null)
                {
                    if (perfilUsuario == "Tecnico")
                    {
                        // PORTA 1: Só para Técnicos
                        await Shell.Current.GoToAsync($"//{nameof(TecnicoDashboardPage)}");
                    }
                    else
                    {
                        // PORTA 2: Para todos os outros (Alunos/Colaboradores)
                        await Shell.Current.GoToAsync("//MainAppTabs");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro", "Usuário ou senha inválidos.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }
    }
}