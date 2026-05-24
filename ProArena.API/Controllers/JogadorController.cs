using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;
using ProArena.Domain.Enums;

namespace ProArena.API.Controllers
{
    [Route("api/jogador")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JogadorController : ControllerBase
    {
        private readonly IJogadorService _jogadorService;

        public JogadorController(IJogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }

        [HttpGet("busca-todos-jogadores")]
        public async Task<IActionResult> BuscaTodosJogadores()
        {
            var resultado = await _jogadorService.BuscaTodosJogadores();
            return TrataResultado(resultado);
        }

        [HttpGet("busca-jogador-{id}")]
        public async Task<IActionResult> BuscaJogadorPorId(int id)
        {
            var resultado = await _jogadorService.BuscaJogadorPorId(id);
            return TrataResultado(resultado);
        }

        [HttpPost("registra-jogador")]
        public async Task<IActionResult> RegistraJogador(RegistraJogadorDTO registraJogadorDTO)
        {
            if (registraJogadorDTO is null)
            {
                return BadRequest();
            }

            var resultado = await _jogadorService.RegistraJogador(registraJogadorDTO);
            return TrataResultado(resultado, created: true);
        }

        [HttpPut("atualiza-jogador")]
        public async Task<IActionResult> AtualizaJogador(AtualizaJogadorDTO atualizaJogadorDTO)
        {
            if (atualizaJogadorDTO is null)
            {
                return BadRequest();
            }

            var resultado = await _jogadorService.AtualizaJogador(atualizaJogadorDTO);
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
