using PIM4App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            // SIMULAÇÃO: No seu PIM real, isso viria da API ASP.NET
            await Task.Delay(1000); // Simula espera da rede

            // Login de USUÁRIO comum
            if (username.ToLower() == "aluno" && password == "12345")
            {
                return "Usuario";
            }

            // Login de TÉCNICO
            if (username.ToLower() == "tecnico" && password == "admin")
            {
                return "Tecnico";
            }

            // Login falhou
            return null;
        }
    }
}
