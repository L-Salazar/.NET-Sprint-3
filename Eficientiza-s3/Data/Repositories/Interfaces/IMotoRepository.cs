using Eficientiza_s3.Models;

namespace Eficientiza_s3.Data.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        Task<PageData<MotoEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10);
        Task<MotoEntity?> ObterUmaAsync(int Id);
        Task<MotoEntity?> AdicionarAsync(MotoEntity entity);
        Task<MotoEntity?> EditarAsync(int Id, MotoEntity entity);
        Task<MotoEntity?> DeletarAsync(int Id);
        Task<int> ObterTotalAsync();
    }
}
