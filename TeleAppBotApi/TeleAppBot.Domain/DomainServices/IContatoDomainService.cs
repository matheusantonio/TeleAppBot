namespace TeleAppBot.Domain.DomainServices
{
    public interface IContatoDomainService
    {
        Task ValidarExistenciaDeContato(long idUsuario, bool eBot, string nome, string sobrenome, string usuario);

        Task<bool> ValidarExistenciaDeContato(long idUsuario);
    }
}
