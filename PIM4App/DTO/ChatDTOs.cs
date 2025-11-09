using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM4App.DTO
{
    public class ChatRequest
    {
        public string Pergunta { get; set; }
        public string SessionId { get; internal set; }
    }
    public class IaResponse
    {
        public string Resposta { get; set; }
    }
}
