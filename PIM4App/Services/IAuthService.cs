using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Services
{
    public interface IAuthService
    {
        // Agora retorna uma string com o perfil (ex: "Tecnico", "Usuario")
        // ou null se o login falhar.
        Task<string> LoginAsync(string username, string password);
    }
}
