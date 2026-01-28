using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs;
using ProArena.Application.Enums;
using ProArena.Application.Interfaces;

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

        [HttpGet]
        [Route("busca-jogador-{id}")]
        public async Task<IActionResult> BuscaJogadorPorId(int id)
        {
            var resultadoOperacao = await _jogadorService.BuscaJogadorPorId(id);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacao.Inesperado)
            {
                return StatusCode(500);
            }

            return Ok(resultadoOperacao);
        }

        [HttpPost]
        [Route("registra-jogador")]
        public async Task<IActionResult> RegistraJogador(RegistraJogadorDTO registraJogadorDTO)
        {
            if (registraJogadorDTO is null)
            {
                return BadRequest();
            }

            var resultadoOperacao = await _jogadorService.RegistraJogador(registraJogadorDTO);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacao.Inesperado)
            {
                return StatusCode(500);
            }

            return Created();
        }

        [HttpPut]
        [Route("atualiza-jogador")]
        public async Task<IActionResult> AtualizaJogador(AtualizaJogadorDTO atualizaJogadorDTO)
        {
            if (atualizaJogadorDTO is null)
            {
                return BadRequest();
            }

            var resultadoOperacao = await _jogadorService.AtualizaJogador(atualizaJogadorDTO);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacao.Inesperado)
            {
                return StatusCode(500);
            }

            return Created();
        }
    }
}
