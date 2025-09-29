namespace Eficientiza_s3.Dtos
{
    public record MotoDto(
        string Placa,
        string Modelo,
        string? Cor,
        int Ano,
        string Status
    );
}
