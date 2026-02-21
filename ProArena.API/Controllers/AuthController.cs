using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;

namespace ProArena.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login-usuario")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            var resultadoOperacao = await _authService.Login(loginUsuarioDTO);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacaoEnum.NaoAutorizado)
            {
                return Unauthorized(resultadoOperacao);
            }

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacaoEnum.NaoEncontrado)
            {
                return NotFound(resultadoOperacao);
            }

            return Ok(resultadoOperacao);
        }
    }
}
