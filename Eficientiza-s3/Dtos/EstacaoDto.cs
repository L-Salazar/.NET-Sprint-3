namespace Eficientiza_s3.Dtos
{
    public record EstacaoDto(
        string Nome,
        string? Localizacao,
        int? Capacidade,
        string Status,
        DateTime? DataCriacao,
        DateTime? DataUltimaAtualizacao,
        string? Responsavel
    );
}
