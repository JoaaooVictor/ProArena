using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Campeonatos;
using ProArena.Application.Interfaces;
using ProArena.Domain.Enums;

namespace ProArena.API.Controllers
{
    [ApiController]
    [Route("api/partida")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService _service;

        public PartidaController(IPartidaService service)
        {
            _service = service;
        }

        [HttpGet("busca-todos")]
        public async Task<IActionResult> BuscaTodos()
        {
            var resultado = await _service.BuscaTodos();
            return TrataResultado(resultado);
        }

        [HttpGet("busca-por-id")]
        public async Task<IActionResult> BuscaPorId([FromQuery] int id)
        {
            var resultado = await _service.BuscaPorId(id);
            return TrataResultado(resultado);
        }

        [HttpGet("busca-por-campeonato")]
        public async Task<IActionResult> BuscaPorCampeonato([FromQuery] int campeonatoId)
        {
            var resultado = await _service.BuscaPorCampeonato(campeonatoId);
            return TrataResultado(resultado);
        }

        [HttpPost("registra")]
        public async Task<IActionResult> Registra(RegistraPartidaDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _service.RegistraPartida(dto);
            return TrataResultado(resultado, created: true);
        }

        [HttpPut("atualiza")]
        public async Task<IActionResult> Atualiza(AtualizaPartidaDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _service.AtualizaPartida(dto);
            return TrataResultado(resultado);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var resultado = await _service.Remove(id);
            return TrataResultado(resultado);
        }

        private IActionResult TrataResultado(Application.Utils.ResultadoOperacao resultado, bool created = false)
        {
            if (resultado.Erro && resultado.TipoErro == TipoErroOperacaoEnum.NaoEncontrado)
            {
                return NotFound(resultado);
            }

            if (resultado.Erro && resultado.TipoErro == TipoErroOperacaoEnum.Inesperado)
            {
                return StatusCode(500, resultado);
            }

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            return created ? Created(string.Empty, resultado) : Ok(resultado);
        }
    }
}
