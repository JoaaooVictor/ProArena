using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Application.Enums;
using ProArena.Application.Interfaces;

namespace ProArena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            var resultadoOperacao = await _authService.Login(loginUsuarioDTO);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacao.NaoAutorizado)
            {
                return Unauthorized(resultadoOperacao);
            }

            return Ok(resultadoOperacao);
        }
    }
}
