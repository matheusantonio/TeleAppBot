namespace TeleAppBot.Domain.Entities.Contatos
{
    public class Contato : Entity, IAggregateRoot
    {
        public Contato(int idUsuario, bool eBot, string nome, string sobrenome, string usuario)
        {
            IdUsuario = idUsuario;
            EBot = eBot;
            Nome = nome;
            Sobrenome = sobrenome;
            Usuario = usuario;
        }

        public int IdUsuario { get; private set; }

        public bool EBot { get; private set; }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Usuario { get; private set; }
    }
}
