namespace TeleAppBot.Domain.Entities
{
    public class Entity : IEntity
    {
        public Guid Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
