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
    [Route("api/v1/estacao")]
    [ApiController]
    public class EstacoesController : ControllerBase
    {
        private readonly IEstacaoRepository _estacaoRepository;

        public EstacoesController(IEstacaoRepository estacaoRepository)
        {
            _estacaoRepository = estacaoRepository;
        }

        // GET api/v1/estacao
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista estações",
            Description = "Retorna a lista paginada de estações cadastradas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de estações retornada com sucesso", type: typeof(IEnumerable<EstacaoEntity>))]
        [SwaggerResponse(statusCode: 204, description: "Não há estações cadastradas")]
        [SwaggerResponseExample(statusCode: 200, typeof(EstacaoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var result = await _estacaoRepository.ObterTodosAsync(PaginaAtual, LimitePagina);

            if (!result.Itens.Any())
                return NoContent();

            var hateaos = new
            {
                data = result.Itens.Select(e => new
                {
                    e.Nome,
                    e.Localizacao,
                    e.Capacidade,
                    e.Status,
                    e.DataCriacao,
                    e.DataUltimaAtualizacao,
                    e.Responsavel,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Estacoes", new { id = e.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Estacoes", new { id = e.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Estacoes", new { id = e.Id }, Request.Scheme)
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Estacoes", new { PaginaAtual, LimitePagina }, Request.Scheme),
                    create = Url.Action(nameof(Post), "Estacoes", null, Request.Scheme),
                    first = Url.Action(nameof(Get), "Estacoes", new { PaginaAtual = 1, LimitePagina }, Request.Scheme),
                    prev = PaginaAtual > 1
                                ? Url.Action(nameof(Get), "Estacoes", new { PaginaAtual = PaginaAtual - 1, LimitePagina }, Request.Scheme)
                                : null,
                    next = PaginaAtual < result.TotalPaginas
                                ? Url.Action(nameof(Get), "Estacoes", new { PaginaAtual = PaginaAtual + 1, LimitePagina }, Request.Scheme)
                                : null,
                    last = Url.Action(nameof(Get), "Estacoes", new { PaginaAtual = result.TotalPaginas, LimitePagina }, Request.Scheme)
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

        // GET api/v1/estacao/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém estação por ID",
            Description = "Retorna a estação correspondente ao ID informado."
        )]
        [SwaggerResponse(statusCode: 200, description: "Estação encontrada", type: typeof(EstacaoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Estação não encontrada")]
        [SwaggerResponseExample(statusCode: 200, typeof(EstacaoResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _estacaoRepository.ObterUmaAsync(id);

            if (result is null)
                return NotFound();

            var hateaos = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Estacoes", new { id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Estacoes", null, Request.Scheme),
                    update = Url.Action(nameof(Put), "Estacoes", new { id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Estacoes", new { id }, Request.Scheme),
                }
            };

            return Ok(hateaos);
        }

        // POST api/v1/estacao
        [HttpPost]
        [SwaggerRequestExample(typeof(EstacaoDto), typeof(EstacaoRequestSample))]
        [SwaggerResponse(statusCode: 201, description: "Estação criada com sucesso", type: typeof(EstacaoEntity))]
        [SwaggerResponseExample(statusCode: 201, typeof(EstacaoResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post([FromBody] EstacaoDto dto)
        {
            try
            {
                var entity = dto.ToEstacaoEntity();
                var result = await _estacaoRepository.AdicionarAsync(entity);

                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Estacoes", new { id = result.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Estacoes", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Estacoes", new { id = result.Id }, Request.Scheme),
                    }
                };

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, hateaos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar estação: {ex.Message}");
                return BadRequest("Erro ao criar estação.");
            }
        }

        // PUT api/v1/estacao/{id}
        [HttpPut("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Estação atualizada com sucesso", type: typeof(EstacaoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Estação não encontrada")]
        [SwaggerRequestExample(typeof(EstacaoDto), typeof(EstacaoRequestUpdateSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, [FromBody] EstacaoDto dto)
        {
            var result = await _estacaoRepository.EditarAsync(id, dto.ToEstacaoEntity());

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/v1/estacao/{id}
        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Estação deletada com sucesso", type: typeof(EstacaoEntity))]
        [SwaggerResponse(statusCode: 404, description: "Estação não encontrada")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _estacaoRepository.DeletarAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
