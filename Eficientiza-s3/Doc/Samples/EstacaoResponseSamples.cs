using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Eficientiza_s3.Doc.Samples
{
    public class EstacaoResponseListSample : IExamplesProvider<IEnumerable<EstacaoEntity>>
    {
        public IEnumerable<EstacaoEntity> GetExamples()
        {
            return new List<EstacaoEntity>
            {
                new EstacaoEntity
                {
                    Id = 1,
                    Nome = "Estação Paulista",
                    Localizacao = "Av. Paulista, 1000 - São Paulo, SP",
                    Capacidade = 50,
                    Status = "Ativa",
                    DataCriacao = new DateTime(2020, 5, 10),
                    DataUltimaAtualizacao = new DateTime(2023, 10, 1),
                    Responsavel = "João Silva"
                },
                new EstacaoEntity
                {
                    Id = 2,
                    Nome = "Estação Lapa",
                    Localizacao = "Rua 12 de Outubro, 500 - São Paulo, SP",
                    Capacidade = 30,
                    Status = "Manutenção",
                    DataCriacao = new DateTime(2018, 8, 22),
                    DataUltimaAtualizacao = new DateTime(2023, 9, 15),
                    Responsavel = "Maria Souza"
                }
            };
        }
    }

    public class EstacaoResponseSample : IExamplesProvider<EstacaoEntity>
    {
        public EstacaoEntity GetExamples()
        {
            return new EstacaoEntity
            {
                Id = 1,
                Nome = "Estação Paulista",
                Localizacao = "Av. Paulista, 1000 - São Paulo, SP",
                Capacidade = 50,
                Status = "Ativa",
                DataCriacao = new DateTime(2020, 5, 10),
                DataUltimaAtualizacao = new DateTime(2023, 10, 1),
                Responsavel = "João Silva"
            };
        }
    }

    public class EstacaoRequestUpdateSample : IExamplesProvider<EstacaoDto>
    {
        public EstacaoDto GetExamples()
        {
            return new EstacaoDto(
                "Estação Paulista Atualizada",
                "Av. Paulista, 2000 - São Paulo, SP",
                60,
                "Ativa",
                DateTime.Now.AddYears(-2),
                DateTime.Now,
                "Carlos Andrade"
            );
        }
    }

    public class EstacaoRequestInvalidSample : IExamplesProvider<EstacaoDto>
    {
        public EstacaoDto GetExamples()
        {
            return new EstacaoDto(
                "",     // Nome vazio (inválido)
                "",     // Localização vazia (inválido)
                null,   // Capacidade nula
                "",     // Status vazio
                null,   // Data criação nula
                null,   // Última atualização nula
                null    // Responsável nulo
            );
        }
    }

    public class EstacaoRequestSample : IExamplesProvider<EstacaoDto>
    {
        public EstacaoDto GetExamples()
        {
            return new EstacaoDto(
                "Estação Centro",
                "Rua XV de Novembro, 123 - Campinas, SP",
                40,
                "Ativa",
                new DateTime(2021, 1, 10),
                new DateTime(2023, 9, 1),
                "Ana Lima"
            );
        }
    }
}
