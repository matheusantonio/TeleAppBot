namespace TeleAppBot.Domain.DomainServices
{
    public interface IContatoDomainService
    {
        Task ValidarExistenciaDeContato(int idUsuario, bool eBot, string nome, string sobrenome, string usuario);

        Task<bool> ValidarExistenciaDeContato(int idUsuario);
    }
}
