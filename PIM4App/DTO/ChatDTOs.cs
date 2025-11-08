using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.DTO
{
    // O que o App Mobile ENVIA para o Backend
    public class ChatRequest
    {
        public string Pergunta { get; set; }
        public string SessionId { get; internal set; }
    }

    // O que o App Mobile RECEBE de volta do Backend
    public class IaResponse
    {
        public string Resposta { get; set; }
    }
}
