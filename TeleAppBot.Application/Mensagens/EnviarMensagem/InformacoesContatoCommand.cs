using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem
{
    public record InformacoesContatoCommand(bool EBot, string Nome, string Sobrenome, string Usuario);
}
