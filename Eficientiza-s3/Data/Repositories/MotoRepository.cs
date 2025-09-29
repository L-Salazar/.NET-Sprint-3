using Microsoft.EntityFrameworkCore;
using Eficientiza_s3.Data.Repositories.Interfaces;
using Eficientiza_s3.Models;
using Eficientiza_s3.Data.AppData;

namespace Eficientiza_s3.Data.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationContext _context;

        public MotoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<MotoEntity?> AdicionarAsync(MotoEntity entity)
        {
            _context.Motos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MotoEntity?> DeletarAsync(int Id)
        {
            var result = await _context.Motos.FindAsync(Id);

            if (result is not null)
            {
                _context.Motos.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<MotoEntity?> EditarAsync(int Id, MotoEntity entity)
        {
            var result = await _context.Motos
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (result is not null)
            {
                result.Placa = entity.Placa;
                result.Modelo = entity.Modelo;
                result.Cor = entity.Cor;
                result.Ano = entity.Ano;
                result.Status = entity.Status;

                _context.Motos.Update(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PageData<MotoEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var totalRegistros = await _context.Motos.CountAsync();

            var result = await _context.Motos
                .OrderBy(x => x.Id)
                .Skip((PaginaAtual - 1) * LimitePagina)
                .Take(LimitePagina)
                .ToListAsync();

            var totalPaginas = (totalRegistros + LimitePagina - 1) / LimitePagina;

            return new PageData<MotoEntity>(PaginaAtual, totalPaginas, totalRegistros, result);
        }

        public async Task<MotoEntity?> ObterUmaAsync(int Id)
        {
            var result = await _context.Motos
                .FirstOrDefaultAsync(x => x.Id == Id);

            return result;
        }

        public async Task<int> ObterTotalAsync()
        {
            return await _context.Motos.CountAsync();
        }
    }
}
