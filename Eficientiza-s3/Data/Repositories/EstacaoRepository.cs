using Microsoft.EntityFrameworkCore;
using Eficientiza_s3.Data.Repositories.Interfaces;
using Eficientiza_s3.Models;
using Eficientiza_s3.Data.AppData;

namespace Eficientiza_s3.Data.Repositories
{
    public class EstacaoRepository : IEstacaoRepository
    {
        private readonly ApplicationContext _context;

        public EstacaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<EstacaoEntity?> AdicionarAsync(EstacaoEntity entity)
        {
            _context.Estacoes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EstacaoEntity?> DeletarAsync(int Id)
        {
            var result = await _context.Estacoes.FindAsync(Id);

            if (result is not null)
            {
                _context.Estacoes.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<EstacaoEntity?> EditarAsync(int Id, EstacaoEntity entity)
        {
            var result = await _context.Estacoes
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (result is not null)
            {
                result.Nome = entity.Nome;
                result.Localizacao = entity.Localizacao;
                result.Capacidade = entity.Capacidade;
                result.Status = entity.Status;
                result.DataCriacao = entity.DataCriacao;
                result.DataUltimaAtualizacao = entity.DataUltimaAtualizacao;
                result.Responsavel = entity.Responsavel;

                _context.Estacoes.Update(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PageData<EstacaoEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var totalRegistros = await _context.Estacoes.CountAsync();

            var result = await _context.Estacoes
                .OrderBy(x => x.Id)
                .Skip((PaginaAtual - 1) * LimitePagina)
                .Take(LimitePagina)
                .ToListAsync();

            var totalPaginas = (totalRegistros + LimitePagina - 1) / LimitePagina;

            return new PageData<EstacaoEntity>(PaginaAtual, totalPaginas, totalRegistros, result);
        }

        public async Task<EstacaoEntity?> ObterUmaAsync(int Id)
        {
            var result = await _context.Estacoes
                .FirstOrDefaultAsync(x => x.Id == Id);

            return result;
        }

        public async Task<int> ObterTotalAsync()
        {
            return await _context.Estacoes.CountAsync();
        }
    }
}
