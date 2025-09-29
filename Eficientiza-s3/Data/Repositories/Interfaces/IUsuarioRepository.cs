using Eficientiza_s3.Models;

namespace Eficientiza_s3.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PageData<UsuarioEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10);
        Task<UsuarioEntity?> ObterUmaAsync(int Id);
        Task<UsuarioEntity?> AdicionarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> EditarAsync(int Id, UsuarioEntity entity);
        Task<UsuarioEntity?> DeletarAsync(int Id);
        Task<int> ObterTotalAsync();
    }
}
