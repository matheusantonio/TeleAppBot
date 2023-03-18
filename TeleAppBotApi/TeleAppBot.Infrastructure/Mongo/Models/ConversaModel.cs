namespace TeleAppBot.Infrastructure.Mongo.Models
{
    public class ConversaModel
    {
        public Guid Id { get; set; }
        public long IdChat { get; set; }
        public Guid IdContato { get; set; }
        public bool Invertida { get; set; }
    }
}
