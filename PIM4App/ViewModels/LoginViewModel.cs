using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PIM4App.Services;
using PIM4App.Views; // Para o nome das páginas

namespace PIM4App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _username; // (O XAML está ligado nisto, mas vamos usar como Email)

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _isBusy; // Para mostrar um "carregando"

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand(CanExecute = nameof(IsNotBusy))]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preencha o e-mail e a senha.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var loginResponse = await _authService.LoginAsync(Username, Password);

                if (loginResponse != null)
                {
                    // Roteamento baseado no Perfil (vindo da API)
                    if (loginResponse.Perfil == "Tecnico")
                    {
                        await Shell.Current.GoToAsync($"//{nameof(TecnicoDashboardPage)}");
                    }
                    else // ("Colaborador" ou qualquer outro)
                    {
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
                await Shell.Current.DisplayAlert("Erro de Conexão", $"Não foi possível conectar ao servidor: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool IsNotBusy() => !IsBusy;
    }
}