namespace PIM4App.Models
{
    // Este modelo agora espelha o ChamadoDTO que o Backend nos envia
    public class Chamado
    {
        public int IdChamado { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int Prioridade { get; set; } // Corrigido para int
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int? IdCategoria { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public int? IdTecnicoResponsavel { get; set; }
        public int? IdStatus { get; set; } // Corrigido para int?
    }
}