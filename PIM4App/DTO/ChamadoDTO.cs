namespace PIM4App.DTO
{
    // DTO para LER um chamado (espelha o DTO do seu backend)
    public class ChamadoDTO
    {
        [System.Text.Json.Serialization.JsonPropertyName("idChamado")]
        public int IdChamado { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("titulo")]
        public string? Titulo { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("descricao")]
        public string? Descricao { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("prioridade")]
        public string Prioridade { get; set; } // Corrigido para int
        [System.Text.Json.Serialization.JsonPropertyName("dataAbertura")]
        public DateTime DataAbertura { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("dataFechamento")]
        public DateTime? DataFechamento { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("idCategoria")]
        public int? IdCategoria { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("idUsuarioSolicitante")]
        public int IdUsuarioSolicitante { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("idTecnicoResponsavel")]
        public int? IdTecnicoResponsavel { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("idStatus")]
        public int? IdStatus { get; set; }
    }
}