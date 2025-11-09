using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PIM4App.DTO
{
    public class ComentarioDTO
    {
        public int IdChamado { get; set; }
        public string? Mensagem { get; set; }
    }
}
