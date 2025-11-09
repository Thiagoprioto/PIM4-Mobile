using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PIM4App.DTO
{
    public class InteracaoDTO
    {
        [JsonPropertyName("idInteracao")]
        public int IdInteracao { get; set; }

        [JsonPropertyName("idChamado")]
        public int IdChamado { get; set; }

        [JsonPropertyName("idAutor")]
        public int IdAutor { get; set; }

        [JsonPropertyName("mensagem")]
        public string? Mensagem { get; set; }

        [JsonPropertyName("dataInteracao")]
        public DateTime DataInteracao { get; set; }
    }
}