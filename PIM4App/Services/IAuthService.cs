using PIM4App.DTO;

namespace PIM4App.Services
{
    public interface IAuthService
    {
        // Retorna o DTO de resposta ou 'null' se o login falhar.
        Task<LoginResponseDTO> LoginAsync(string email, string password);

        // Método para fazer Logout
        Task LogoutAsync();
    }
}