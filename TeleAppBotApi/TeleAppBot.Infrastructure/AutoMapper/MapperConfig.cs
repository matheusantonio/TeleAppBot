using AutoMapper;
using TeleAppBot.Domain.Entities.Contatos;
using TeleAppBot.Domain.Entities.Conversas;
using TeleAppBot.Domain.Entities.Mensagens;
using TeleAppBot.Infrastructure.Mongo.Models;

namespace TeleAppBot.Infrastructure.AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InicializarAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MensagemModel, Mensagem>().ReverseMap();
                cfg.CreateMap<ConversaModel, Conversa>().ReverseMap();
                cfg.CreateMap<ContatoModel, Contato>().ReverseMap();
            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
