using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.DTO
{
    // O que o App RECEBE
    public class LoginResponseDTO
    {
        [System.Text.Json.Serialization.JsonPropertyName("token")]
        public string? Token { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("perfil")]
        public string? Perfil { get; set; }
    }
}
