using Eficientiza_s3.Dtos;
using Eficientiza_s3.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Eficientiza_s3.Doc.Samples
{
    public class MotoResponseListSample : IExamplesProvider<IEnumerable<MotoEntity>>
    {
        public IEnumerable<MotoEntity> GetExamples()
        {
            return new List<MotoEntity>
            {
                new MotoEntity
                {
                    Id = 1,
                    Placa = "ABC1D23",
                    Modelo = "Honda CG 160",
                    Cor = "Vermelha",
                    Ano = 2020,
                    Status = "Disponível"
                },
                new MotoEntity
                {
                    Id = 2,
                    Placa = "XYZ9H87",
                    Modelo = "Yamaha Fazer 250",
                    Cor = "Preta",
                    Ano = 2022,
                    Status = "Em manutenção"
                },
                new MotoEntity
                {
                    Id = 3,
                    Placa = "JKL5M43",
                    Modelo = "Honda Biz 125",
                    Cor = "Branca",
                    Ano = 2019,
                    Status = "Disponível"
                },
                new MotoEntity
                {
                    Id = 4,
                    Placa = "DEF4G56",
                    Modelo = "Suzuki Yes 125",
                    Cor = "Azul",
                    Ano = 2018,
                    Status = "Indisponível"
                }
            };
        }
    }

    public class MotoResponseSample : IExamplesProvider<MotoEntity>
    {
        public MotoEntity GetExamples()
        {
            return new MotoEntity
            {
                Id = 1,
                Placa = "ABC1D23",
                Modelo = "Honda CG 160",
                Cor = "Vermelha",
                Ano = 2020,
                Status = "Disponível"
            };
        }
    }

    public class MotoRequestUpdateSample : IExamplesProvider<MotoDto>
    {
        public MotoDto GetExamples()
        {
            return new MotoDto(
                "XYZ9H87",          // Nova placa
                "Yamaha Fazer 250", // Novo modelo
                "Preta",            // Nova cor
                2022,               // Novo ano
                "Em manutenção"     // Novo status
            );
        }
    }

    public class MotoRequestInvalidSample : IExamplesProvider<MotoDto>
    {
        public MotoDto GetExamples()
        {
            return new MotoDto(
                "",          // Placa vazia (inválido)
                "",          // Modelo vazio (inválido)
                null,        // Cor nula
                0,           // Ano inválido
                ""           // Status vazio
            );
        }
    }

    public class MotoRequestSample : IExamplesProvider<MotoDto>
    {
        public MotoDto GetExamples()
        {
            return new MotoDto(
                "DEF4G56",       // Placa
                "Suzuki Yes 125",// Modelo
                "Azul",          // Cor
                2018,            // Ano
                "Disponível"     // Status
            );
        }
    }
}
