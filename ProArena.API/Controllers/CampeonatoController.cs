using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;

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

        [HttpGet]
        [Route("busca-campeonato-id")]
        public async Task<IActionResult> BuscaCampeonatoPorId(int id)
        {
            var resultadoOperacao = await _campeonatoService.BuscaCampeonatoPorId(id);

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
            var resultadoOperacao = await _campeonatoService.AdicionaCampeonato(registraCampeonatoDTO);

            if (resultadoOperacao.Erro)
            {
                return NotFound(resultadoOperacao.Mensagem);
            }

            return Ok(resultadoOperacao);
        }

    }
}
