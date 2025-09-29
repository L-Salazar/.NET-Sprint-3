using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;

namespace Eficientiza_s3.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioEntity ToUsuarioEntity(this UsuarioDto obj)
        {
            return new UsuarioEntity
            {
                Nome = obj.Nome,
                Email = obj.Email,
                Senha = obj.Senha,
                TipoUsuario = obj.TipoUsuario
            };
        }

        public static UsuarioDto ToUsuarioDto(this UsuarioEntity obj)
        {
            return new UsuarioDto(
                obj.Nome,
                obj.Email,
                obj.Senha,
                obj.TipoUsuario
            );
        }
    }
}
