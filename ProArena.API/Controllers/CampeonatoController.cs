using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;
using ProArena.Domain.Enums;

namespace ProArena.API.Controllers
{
    [ApiController]
    [Route("api/campeonato")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CampeonatoController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;

        public CampeonatoController(ICampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpGet("busca-todos-campeonatos")]
        public async Task<IActionResult> BuscaCampeonatos()
        {
            var resultado = await _campeonatoService.BuscaTodosCampeonatos();
            return TrataResultado(resultado);
        }

        [HttpGet("busca-campeonato-id")]
        public async Task<IActionResult> BuscaCampeonatoPorId([FromQuery] int id)
        {
            var resultado = await _campeonatoService.BuscaCampeonatoPorId(id);
            return TrataResultado(resultado);
        }

        [HttpPost("cria-campeonato")]
        public async Task<IActionResult> CriaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO)
        {
            var resultado = await _campeonatoService.AdicionaCampeonato(registraCampeonatoDTO);
            return TrataResultado(resultado, created: true);
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
