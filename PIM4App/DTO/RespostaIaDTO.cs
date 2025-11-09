using System;
using System.Text.Json.Serialization;

namespace PIM4App.DTO
{
    // Esta classe espelha a tabela RespostasIA do seu Backend
    public class RespostaIaDTO
    {
        [JsonPropertyName("idRespostaIA")]
        public int IdRespostaIA { get; set; }

        [JsonPropertyName("idChamado")]
        public int IdChamado { get; set; }

        [JsonPropertyName("mensagem")]
        public string? Mensagem { get; set; }

        [JsonPropertyName("modelo")]
        public string? Modelo { get; set; }

        [JsonPropertyName("dataGeracao")]
        public DateTime DataGeracao { get; set; }
    }
}
