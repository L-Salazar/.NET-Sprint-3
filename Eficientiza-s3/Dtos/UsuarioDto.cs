namespace Eficientiza_s3.Dtos
{
    public record UsuarioDto(
        string Nome,
        string Email,
        string Senha,
        string TipoUsuario
    );
}
