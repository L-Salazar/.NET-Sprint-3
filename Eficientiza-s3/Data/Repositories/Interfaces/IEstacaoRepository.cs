using Eficientiza_s3.Models;

namespace Eficientiza_s3.Data.Repositories.Interfaces
{
    public interface IEstacaoRepository
    {
        Task<PageData<EstacaoEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10);
        Task<EstacaoEntity?> ObterUmaAsync(int Id);
        Task<EstacaoEntity?> AdicionarAsync(EstacaoEntity entity);
        Task<EstacaoEntity?> EditarAsync(int Id, EstacaoEntity entity);
        Task<EstacaoEntity?> DeletarAsync(int Id);
        Task<int> ObterTotalAsync();
    }
}
