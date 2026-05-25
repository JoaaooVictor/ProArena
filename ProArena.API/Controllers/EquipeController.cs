using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Equipes;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Enums;

namespace ProArena.API.Controllers
{
    [ApiController]
    [Route("api/equipe")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeService _equipeService;

        public EquipeController(IEquipeService service)
        {
            _equipeService = service;
        }

        [HttpGet("busca-todos")]
        public async Task<IActionResult> BuscaTodos()
        {
            var resultado = await _equipeService.BuscaTodos();
            return TrataResultado(resultado);
        }

        [HttpGet("busca-por-id")]
        public async Task<IActionResult> BuscaPorId([FromQuery] int id)
        {
            var resultado = await _equipeService.BuscaPorId(id);
            return TrataResultado(resultado);
        }

        [HttpPost("registra")]
        public async Task<IActionResult> Registra(RegistraEquipeDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _equipeService.RegistraEquipe(dto);
            return TrataResultado(resultado, created: true);
        }

        [HttpPut("atualiza")]
        public async Task<IActionResult> Atualiza(AtualizaEquipeDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _equipeService.AtualizaEquipe(dto);
            return TrataResultado(resultado);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var resultado = await _equipeService.Remove(id);
            return TrataResultado(resultado);
        }

        private IActionResult TrataResultado(ResultadoOperacao resultado, bool created = false)
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
