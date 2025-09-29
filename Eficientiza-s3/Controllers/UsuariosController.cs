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
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET api/v1/usuario
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista usuários",
            Description = "Retorna a lista paginada de usuários cadastrados."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de usuários retornada com sucesso", type: typeof(IEnumerable<UsuarioEntity>))]
        [SwaggerResponse(statusCode: 204, description: "Não há usuários cadastrados")]
        [SwaggerResponseExample(statusCode: 200, typeof(UsuarioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int PaginaAtual = 1, int LimitePagina = 10)
        {
            var result = await _usuarioRepository.ObterTodosAsync(PaginaAtual, LimitePagina);

            if (!result.Itens.Any())
                return NoContent();

            var hateaos = new
            {
                data = result.Itens.Select(u => new
                {
                    u.Nome,
                    u.Email,
                    u.Senha,
                    u.TipoUsuario,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Usuarios", new { id = u.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Usuarios", new { id = u.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Usuarios", new { id = u.Id }, Request.Scheme)
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Usuarios", new { PaginaAtual, LimitePagina }, Request.Scheme),
                    create = Url.Action(nameof(Post), "Usuarios", null, Request.Scheme),
                    first = Url.Action(nameof(Get), "Usuarios", new { PaginaAtual = 1, LimitePagina }, Request.Scheme),
                    prev = PaginaAtual > 1
                                ? Url.Action(nameof(Get), "Usuarios", new { PaginaAtual = PaginaAtual - 1, LimitePagina }, Request.Scheme)
                                : null,
                    next = PaginaAtual < result.TotalPaginas
                                ? Url.Action(nameof(Get), "Usuarios", new { PaginaAtual = PaginaAtual + 1, LimitePagina }, Request.Scheme)
                                : null,
                    last = Url.Action(nameof(Get), "Usuarios", new { PaginaAtual = result.TotalPaginas, LimitePagina }, Request.Scheme)
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

        // GET api/v1/usuario/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém usuário por ID",
            Description = "Retorna o usuário correspondente ao ID informado."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado", type: typeof(UsuarioEntity))]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado")]
        [SwaggerResponseExample(statusCode: 200, typeof(UsuarioResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _usuarioRepository.ObterUmaAsync(id);

            if (result is null)
                return NotFound();

            var hateaos = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Usuarios", new { id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Usuarios", null, Request.Scheme),
                    update = Url.Action(nameof(Put), "Usuarios", new { id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Usuarios", new { id }, Request.Scheme),
                }
            };

            return Ok(hateaos);
        }

        // POST api/v1/usuario
        [HttpPost]
        [SwaggerRequestExample(typeof(UsuarioDto), typeof(UsuarioRequestSample))]
        [SwaggerResponse(statusCode: 201, description: "Usuário criado com sucesso", type: typeof(UsuarioEntity))]
        [SwaggerResponseExample(statusCode: 201, typeof(UsuarioResponseSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post([FromBody] UsuarioDto dto)
        {
            try
            {
                var entity = dto.ToUsuarioEntity();
                var result = await _usuarioRepository.AdicionarAsync(entity);

                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Usuarios", new { id = result.Id }, Request.Scheme),
                        update = Url.Action(nameof(Put), "Usuarios", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Usuarios", new { id = result.Id }, Request.Scheme),
                    }
                };

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, hateaos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
                return BadRequest("Erro ao criar usuário.");
            }
        }

        // PUT api/v1/usuario/{id}
        [HttpPut("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Usuário atualizado com sucesso", type: typeof(UsuarioEntity))]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado")]
        [SwaggerRequestExample(typeof(UsuarioDto), typeof(UsuarioRequestUpdateSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDto dto)
        {
            var result = await _usuarioRepository.EditarAsync(id, dto.ToUsuarioEntity());

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/v1/usuario/{id}
        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: 200, description: "Usuário deletado com sucesso", type: typeof(UsuarioEntity))]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioRepository.DeletarAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
