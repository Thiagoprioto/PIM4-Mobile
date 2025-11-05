using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Services
{
    public interface IAuthService
    {
        // Contrato que simula o login (pedido no seu PIM)
        Task<bool> LoginAsync(string username, string password);
    }
}
