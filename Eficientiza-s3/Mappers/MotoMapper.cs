using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;

namespace Eficientiza_s3.Mappers
{
    public static class MotoMapper
    {
        public static MotoEntity ToMotoEntity(this MotoDto obj)
        {
            return new MotoEntity
            {
                Placa = obj.Placa,
                Modelo = obj.Modelo,
                Cor = obj.Cor,
                Ano = obj.Ano,
                Status = obj.Status
            };
        }

        public static MotoDto ToMotoDto(this MotoEntity obj)
        {
            return new MotoDto(
                obj.Placa,
                obj.Modelo,
                obj.Cor,
                obj.Ano,
                obj.Status
            );
        }
    }
}
