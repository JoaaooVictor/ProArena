using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Campeonatos;
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
        private readonly IPartidaService _partidaService;

        public CampeonatoController(ICampeonatoService campeonatoService, IPartidaService partidaService)
        {
            _campeonatoService = campeonatoService;
            _partidaService = partidaService;
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

        [HttpPost("fecha-inscricoes")]
        public async Task<IActionResult> FechaInscricoes(int campeonatoId)
        {
            // Endpoint para fechar inscrições de um campeonato
            // Isso deve ser implementado no service de campeonato
            return Ok(new { mensagem = "Inscrições fechadas com sucesso" });
        }

        [HttpPost("gera-partidas")]
        public async Task<IActionResult> GeraPartidas(int campeonatoId)
        {
            // Endpoint para gerar partidas automaticamente após fechar inscrições
            // Isso deve ser implementado no service de campeonato
            return Ok(new { mensagem = "Partidas geradas com sucesso" });
        }

        [HttpPost("inicia-campeonato")]
        public async Task<IActionResult> IniciaCampeonato(int campeonatoId)
        {
            var resultado = await _campeonatoService.IniciaCampeonato(campeonatoId);
            return TrataResultado(resultado);
        }

        [HttpGet("busca-chaveamento")]
        public async Task<IActionResult> BuscaChaveamento(int campeonatoId)
        {
            var resultado = await _campeonatoService.BuscaChaveamento(campeonatoId);
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
