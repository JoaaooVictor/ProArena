namespace ProArena.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> BuscaUsuarioPorId(int id);
    }
}
