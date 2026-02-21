using AutoMapper;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Usuario?> BuscaUsuarioPorEmail(string email)
        {
            return await _usuarioRepository.BuscaUsuarioPorEmail(email);
        }

        public async Task<Usuario?> BuscaUsuarioPorId(int id)
        {
            return await _usuarioRepository.BuscaUsuarioPorId(id);
        }

        public async Task<ResultadoOperacao> RegistraUsuario(RegistraUsuarioDTO registraUsuarioDTO)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(registraUsuarioDTO);
                usuario.Senha = await _authService.GeraHashSenha(usuario.Senha);
                await _usuarioRepository.RegistraUsuario(usuario);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }

            return ResultadoOperacao.Concluido("Usuário registrado com sucesso!", TipoErroOperacaoEnum.Nenhum);
        }
    }
}
