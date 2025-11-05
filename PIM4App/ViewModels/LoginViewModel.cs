using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Services;

namespace PIM4App.ViewModels
{
    // [ObservableObject] vem do CommunityToolkit e nos ajuda a notificar a tela
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        // [ObservableProperty] cria as propriedades 'Username' e 'Password'
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        // Pedimos o serviço de login no construtor
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        // [RelayCommand] cria um 'LoginCommand' para o botão da tela
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
                bool success = await _authService.LoginAsync(Username, Password);

                if (success)
                {
                    // Sucesso! Navega para a página de chamados
                    // O PIM pede que o app permita abrir chamados [cite: 56]
                    await Shell.Current.GoToAsync("//MainAppTabs");
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