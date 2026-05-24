using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Movimentacao;
using ProArena.Application.Interfaces;
using ProArena.Domain.Enums;

namespace ProArena.API.Controllers
{
    [ApiController]
    [Route("api/fluxo-caixa")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FluxoCaixaController : ControllerBase
    {
        private readonly IMovimentacaoFinanceiraService _service;

        public FluxoCaixaController(IMovimentacaoFinanceiraService service)
        {
            _service = service;
        }

        [HttpGet("busca-todos")]
        public async Task<IActionResult> BuscaTodos()
        {
            var resultado = await _service.BuscaTodosAsync();
            return TrataResultado(resultado);
        }

        [HttpGet("busca-por-periodo")]
        public async Task<IActionResult> BuscaPorPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var resultado = await _service.BuscaPorPeriodoAsync(inicio, fim);
            return TrataResultado(resultado);
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> BuscaResumo([FromQuery] DateTime? inicio, [FromQuery] DateTime? fim)
        {
            var resultado = await _service.BuscaResumoAsync(inicio, fim);
            return TrataResultado(resultado);
        }

        [HttpGet("busca-por-id")]
        public async Task<IActionResult> BuscaPorId([FromQuery] int id)
        {
            var resultado = await _service.BuscaPorIdAsync(id);
            return TrataResultado(resultado);
        }

        [HttpPost("registra")]
        public async Task<IActionResult> Registra(RegistraMovimentacaoFinanceiraDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _service.RegistraAsync(dto);
            return TrataResultado(resultado, created: true);
        }

        [HttpPut("atualiza")]
        public async Task<IActionResult> Atualiza(AtualizaMovimentacaoFinanceiraDTO dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            var resultado = await _service.AtualizaAsync(dto);
            return TrataResultado(resultado);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var resultado = await _service.RemoveAsync(id);
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
