using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.Interfaces;

namespace ProArena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampeonatoController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;

        public CampeonatoController(ICampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscaCampeonatos()
        {
            var resultadoOperacao = await _campeonatoService.BuscaTodosCampeonatos();

            if (resultadoOperacao.Erro) 
            {
                return NotFound("Nenhum campeonato encontrado.");
            }

            return Ok(resultadoOperacao);
        }
    }
}
