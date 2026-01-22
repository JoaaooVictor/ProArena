using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Usuarios;

namespace ProArena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegistraUsuario(RegistraUsuarioDTO registraUsuarioDTO)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            return Ok();
        }
    }
}
