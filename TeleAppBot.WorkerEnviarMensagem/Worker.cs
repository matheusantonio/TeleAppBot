using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.Domain.DomainServices;
using TeleAppBot.Domain.Events;
using TeleAppBot.Domain.Mensageria;
using TeleAppBot.Domain.Repositories;
using static System.Formats.Asn1.AsnWriter;

namespace TeleAppBot.WorkerEnviarMensagem
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var kafkaService = scope.ServiceProvider.GetRequiredService<IKafkaService>();
            var enviarMensagemDomainService = scope.ServiceProvider.GetRequiredService<IMensagemDomainService>();
            var contatosDomainService = scope.ServiceProvider.GetRequiredService<IContatoDomainService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var mensagem = kafkaService.ConsumirMensagem<EnviarMensagemTextoEvent>(stoppingToken);
                if(mensagem is null)
                    continue;

                await contatosDomainService.ValidarExistenciaDeContato(
                    mensagem.IdContato, 
                    mensagem.InformacoesUsuario.EBot, 
                    mensagem.InformacoesUsuario.Nome, 
                    mensagem.InformacoesUsuario.Sobrenome, 
                    mensagem.InformacoesUsuario.Usuario);

                await enviarMensagemDomainService.ProcessarEnvioDeMensagem(mensagem);
            }

        }
    }
}
