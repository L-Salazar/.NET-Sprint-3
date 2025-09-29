using Eficientiza_s3.Data.Repositories.Interfaces;
using Eficientiza_s3.Doc.Samples;
using Eficientiza_s3.Dtos;
using Eficientiza_s3.Mappers;
using Eficientiza_s3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Linq;

namespace Eficientiza_s3.Controllers
{
    [Route("api/v1/moto")]
    [ApiController]
    public class MotosController : ControllerBase
    {
        private readonly IMotoRepository _motoRepository;

        public MotosController(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        // GET api/v1/moto
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista motos",
            Description = "Retorna a lista paginada de motos cadastradas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de motos retornada com sucesso", type: typeof(IEnumerable<MotoEntity>))]
        [SwaggerResponse(statusCode: 204, description: "Não há motos cadastradas")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var result = await _motoRepository.ObterTodosAsync(PaginaAtual, LimitePagina);

            if (!result.Itens.Any())
                return NoContent();

            var hateaos = new
            {
                data = result.Itens.Select(m => new
                {
                    m.Placa,
                    m.Modelo,
                    m.Cor,
                    m.Ano,
                    m.Status,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Motos", new { id = m.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Motos", new { id = m.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Motos", new { id = m.Id }, Request.Scheme)
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Motos", new { PaginaAtual, LimitePagina }, Request.Scheme),
                    create = Url.Action(nameof(Post), "Motos", null, Request.Scheme),
                    first = Url.Action(nameof(Get), "Motos", new { PaginaAtual = 1, LimitePagina }, Request.Scheme),
                    prev = PaginaAtual > 1
                                ? Url.Action(nameof(Get), "Motos", new { PaginaAtual = PaginaAtual - 1, LimitePagina }, Request.Scheme)
                                : null,
                    next = PaginaAtual < result.TotalPaginas
                                ? Url.Action(nameof(Get), "Motos", new { PaginaAtual = PaginaAtual + 1, LimitePagina }, Request.Scheme)
                                : null,
                    last = Url.Action(nameof(Get), "Motos", new { PaginaAtual = result.TotalPaginas, LimitePagina }, Request.Scheme)
                },
                pagina = new
                {
                    PaginaAtual = result.PaginaAtual,
                    TotalPaginas = result.TotalPaginas,
                    TotalRegistros = result.TotalRegistros
                }
            };

            return Ok(hateaos);
        }

        // GET api/v1/moto/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém moto por ID",
            Description = "Retorna a moto correspondente ao ID informado."
        )]
        [SwaggerResponse(statusCode: 200, description: "Moto encontrada", type: typeof(MotoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _motoRepository.ObterUmaAsync(id);

            if (result is null)
                return NotFound();

            var hateaos = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Motos", new { id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Motos", null, Request.Scheme),
                    update = Url.Action(nameof(Put), "Motos", new { id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Motos", new { id }, Request.Scheme),
                }
            };

            return Ok(hateaos);
        }

        // POST api/v1/moto
        [HttpPost]
        [SwaggerRequestExample(typeof(MotoDto), typeof(MotoRequestSample))]
        [SwaggerResponse(statusCode: 201, description: "Moto criada com sucesso", type: typeof(MotoEntity))]
        [SwaggerResponseExample(statusCode: 201, typeof(MotoResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post([FromBody] MotoDto dto)
        {
            try
            {
                var entity = dto.ToMotoEntity();
                var result = await _motoRepository.AdicionarAsync(entity);

                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Motos", new { id = result.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Motos", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Motos", new { id = result.Id }, Request.Scheme),
                    }
                };

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, hateaos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar moto: {ex.Message}");
                return BadRequest("Erro ao criar moto.");
            }
        }

        // PUT api/v1/moto/{id}
        [HttpPut("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Moto atualizada com sucesso", type: typeof(MotoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada")]
        [SwaggerRequestExample(typeof(MotoDto), typeof(MotoRequestUpdateSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, [FromBody] MotoDto dto)
        {
            var result = await _motoRepository.EditarAsync(id, dto.ToMotoEntity());

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/v1/moto/{id}
        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Moto deletada com sucesso", type: typeof(MotoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _motoRepository.DeletarAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
