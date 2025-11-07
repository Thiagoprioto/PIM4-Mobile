using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAbertura { get; set; }
        public string Status { get; set; }
        public string Categoria { get; set; }

        // NOVO CAMPO: ID do técnico que assumiu (pode ser nulo)
        public int? IdTecnicoResponsavel { get; set; }
    }
}
