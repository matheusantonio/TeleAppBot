namespace TeleAppBot.Infrastructure.Mongo.Models
{
    public class ContatoModel
    {
        public Guid Id { get; set; }
        public int IdUsuario { get; set; }

        public bool EBot { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Usuario { get; set; }
    }
}
