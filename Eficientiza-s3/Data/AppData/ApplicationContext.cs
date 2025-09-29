using Eficientiza_s3.Models;
using Microsoft.EntityFrameworkCore;

namespace Eficientiza_s3.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<MotoEntity> Motos { get; set; }
        public DbSet<EstacaoEntity> Estacoes { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
    }
}
