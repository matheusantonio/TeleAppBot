using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TeleAppBot.Domain.DomainServices;
using TeleAppBot.Domain.Mensageria;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Infrastructure.AutoMapper;
using TeleAppBot.Infrastructure.Mensageria;
using TeleAppBot.Infrastructure.Mongo;
using TeleAppBot.Infrastructure.Mongo.Repositories;

namespace TeleAppBot.CrossCutting.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AdicionarServicos(this IServiceCollection servicos, IConfiguration configuracoes)
        {
            servicos.Configure<KafkaConfig>(configuracoes.GetSection("KafkaConfig"));

            servicos.AddScoped(x => new Context(configuracoes.GetConnectionString("MongoDB")!));

            servicos.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            servicos.AddSingleton<IMapper>(MapperConfig.InicializarAutoMapper());

            servicos.AddScoped<IKafkaService, KafkaService>();

            servicos.AddScoped<IContatoDomainService, ContatoDomainService>();
            servicos.AddScoped<IMensagemDomainService, MensagemDomainService>();

            servicos.AddScoped<IMensagensRepository, MensagensRepository>();
            servicos.AddScoped<IConversasRepository, ConversasRepository>();
            servicos.AddScoped<IContatosRepository, ContatosRepository>();
        }
    }
}
