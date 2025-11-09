namespace PIM4App.DTO
{
    // DTO para CRIAR um chamado
    public class NovoChamadoDTO
    {
        [System.Text.Json.Serialization.JsonPropertyName("titulo")]
        public string? Titulo { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("descricao")]
        public string? Descricao { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("idCategoria")]
        public int IdCategoria { get; set; }
    }
}