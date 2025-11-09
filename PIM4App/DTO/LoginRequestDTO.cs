using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.DTO
{
    // O que o App ENVIA
    public class LoginRequestDTO
    {
        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string? Email { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("senha")]
        public string? Senha { get; set; }
    }
}
