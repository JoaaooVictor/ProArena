using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;

namespace ProArena.API.Controllers
{
    [Route("api/campeonato")]
    [ApiController]
    public class CampeonatoController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;

        public CampeonatoController(ICampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpGet]
        [Route("busca-todos-campeonatos")]
        public async Task<IActionResult> BuscaCampeonatos()
        {
            var resultadoOperacao = await _campeonatoService.BuscaTodosCampeonatos();

            if (resultadoOperacao.Erro)
            {
                return NotFound(resultadoOperacao.Mensagem);
            }

            return Ok(resultadoOperacao);
        }

        [HttpPost]
        [Route("cria-campeonato")]
        public async Task<IActionResult> CriaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO)
        {
            var resultadoOperacao = new ResultadoOperacao(true, ""); /*await _campeonatoService.AdicionaCampeonato();*/

            if (resultadoOperacao.Erro)
            {
                return NotFound(resultadoOperacao.Mensagem);
            }

            return Ok(resultadoOperacao);
        }

    }
}
