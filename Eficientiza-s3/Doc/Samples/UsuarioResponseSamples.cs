using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Eficientiza_s3.Doc.Samples
{
    public class UsuarioResponseListSample : IExamplesProvider<IEnumerable<UsuarioEntity>>
    {
        public IEnumerable<UsuarioEntity> GetExamples()
        {
            return new List<UsuarioEntity>
            {
                new UsuarioEntity
                {
                    Id = 1,
                    Nome = "Felipe Camargo",
                    Email = "felipe.camargo@eficientiza.com",
                    Senha = "123456",
                    TipoUsuario = "Administrador"
                },
                new UsuarioEntity
                {
                    Id = 2,
                    Nome = "Ana Souza",
                    Email = "ana.souza@eficientiza.com",
                    Senha = "senhaSegura!",
                    TipoUsuario = "Operador"
                }
            };
        }
    }

    public class UsuarioResponseSample : IExamplesProvider<UsuarioEntity>
    {
        public UsuarioEntity GetExamples()
        {
            return new UsuarioEntity
            {
                Id = 1,
                Nome = "Felipe Camargo",
                Email = "felipe.camargo@eficientiza.com",
                Senha = "123456",
                TipoUsuario = "Administrador"
            };
        }
    }

    public class UsuarioRequestUpdateSample : IExamplesProvider<UsuarioDto>
    {
        public UsuarioDto GetExamples()
        {
            return new UsuarioDto(
                "Ana Souza Atualizada",
                "ana.atualizada@eficientiza.com",
                "novaSenha@2023",
                "Operador"
            );
        }
    }

    public class UsuarioRequestInvalidSample : IExamplesProvider<UsuarioDto>
    {
        public UsuarioDto GetExamples()
        {
            return new UsuarioDto(
                "",                // Nome vazio (inválido)
                "email-invalido",  // Email inválido
                "",                // Senha vazia
                ""                 // Tipo vazio
            );
        }
    }

    public class UsuarioRequestSample : IExamplesProvider<UsuarioDto>
    {
        public UsuarioDto GetExamples()
        {
            return new UsuarioDto(
                "Carlos Lima",
                "carlos.lima@eficientiza.com",
                "senhaForte123",
                "Operador"
            );
        }
    }
}
