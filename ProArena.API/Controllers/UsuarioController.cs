using Microsoft.AspNetCore.Mvc;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Enums;
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

        /// <summary>
        /// Cria usário de acordo com os parametros fornecidos, não necessita estar logado!
        /// </summary>
        /// <param name="">RegistraUsuarioDTO</param>
        /// <returns>Status Code 201 - Created</returns>
        [HttpPost]
        [Route("registra-usuario")]
        public async Task<IActionResult> RegistraUsuario(RegistraUsuarioDTO registraUsuarioDTO)
        {
            if (registraUsuarioDTO is null)
            {
                return BadRequest();
            }

            var resultadoOperacao = await _usuarioService.RegistraUsuario(registraUsuarioDTO);

            if (resultadoOperacao.Erro && resultadoOperacao.TipoErro == TipoErroOperacaoEnum.Inesperado)
            {
                return StatusCode(500);
            }

            return Created();
        }
    }
}
