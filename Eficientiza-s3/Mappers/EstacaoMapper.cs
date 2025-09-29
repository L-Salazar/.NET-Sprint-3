using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;

namespace Eficientiza_s3.Mappers
{
    public static class EstacaoMapper
    {
        public static EstacaoEntity ToEstacaoEntity(this EstacaoDto obj)
        {
            return new EstacaoEntity
            {
                Nome = obj.Nome,
                Localizacao = obj.Localizacao,
                Capacidade = obj.Capacidade,
                Status = obj.Status,
                DataCriacao = obj.DataCriacao,
                DataUltimaAtualizacao = obj.DataUltimaAtualizacao,
                Responsavel = obj.Responsavel
            };
        }

        public static EstacaoDto ToEstacaoDto(this EstacaoEntity obj)
        {
            return new EstacaoDto(
                obj.Nome,
                obj.Localizacao,
                obj.Capacidade,
                obj.Status,
                obj.DataCriacao,
                obj.DataUltimaAtualizacao,
                obj.Responsavel
            );
        }
    }
}
