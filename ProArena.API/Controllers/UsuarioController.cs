using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Application.Interfaces;

namespace ProArena.API.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("registra-usuario")]
        public async Task<IActionResult> RegistraUsuario(RegistraUsuarioDTO registraUsuarioDTO)
        {
            if (registraUsuarioDTO is null)
            {
                return BadRequest();
            }

            var resultadoOperacao = await _usuarioService.RegistraUsuario(registraUsuarioDTO);

            if (resultadoOperacao.Erro)
            {
                return StatusCode(500);
            }

            return Created();
        }
    }
}
