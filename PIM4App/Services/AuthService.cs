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
        public async Task<bool> LoginAsync(string username, string password)
        {
            // SIMULAÇÃO: No seu PIM, aqui você chamaria sua API ASP.NET 
            await Task.Delay(1000); // Simula espera da rede

            // Simula um login de teste
            if (username.ToLower() == "aluno" && password == "12345")
            {
                return true; // Sucesso
            }

            return false; // Falha
        }
    }
}
