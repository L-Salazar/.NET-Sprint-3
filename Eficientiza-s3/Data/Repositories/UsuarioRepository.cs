using Microsoft.EntityFrameworkCore;
using Eficientiza_s3.Data.Repositories.Interfaces;
using Eficientiza_s3.Models;
using Eficientiza_s3.Data.AppData;

namespace Eficientiza_s3.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UsuarioEntity?> AdicionarAsync(UsuarioEntity entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioEntity?> DeletarAsync(int Id)
        {
            var result = await _context.Usuarios.FindAsync(Id);

            if (result is not null)
            {
                _context.Usuarios.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<UsuarioEntity?> EditarAsync(int Id, UsuarioEntity entity)
        {
            var result = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (result is not null)
            {
                result.Nome = entity.Nome;
                result.Email = entity.Email;
                result.Senha = entity.Senha;
                result.TipoUsuario = entity.TipoUsuario;

                _context.Usuarios.Update(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PageData<UsuarioEntity>> ObterTodosAsync(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var totalRegistros = await _context.Usuarios.CountAsync();

            var result = await _context.Usuarios
                .OrderBy(x => x.Id)
                .Skip((PaginaAtual - 1) * LimitePagina)
                .Take(LimitePagina)
                .ToListAsync();

            var totalPaginas = (totalRegistros + LimitePagina - 1) / LimitePagina;

            return new PageData<UsuarioEntity>(PaginaAtual, totalPaginas, totalRegistros, result);
        }

        public async Task<UsuarioEntity?> ObterUmaAsync(int Id)
        {
            var result = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == Id);

            return result;
        }

        public async Task<int> ObterTotalAsync()
        {
            return await _context.Usuarios.CountAsync();
        }
    }
}
