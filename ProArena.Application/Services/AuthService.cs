using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProArena.Application.DTOs.Usuarios;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProArena.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUsuarioRepository _usuarioRepository;
        public AuthService(IOptions<JwtConfig> jwtConfig, IUsuarioRepository usuarioRepository)
        {
            _jwtConfig = jwtConfig.Value;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> GeraHashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha está vazia", nameof(senha));
            }

            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public async Task<string> GeraTokenJwt(string email)
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Key);
            var issuer = _jwtConfig.Issuer;
            var audience = _jwtConfig.Audience;
            var expireMinutes = _jwtConfig.ExpireMinutes;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Expired, DateTime.UtcNow.AddMinutes(expireMinutes).ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResultadoOperacao> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            if (string.IsNullOrEmpty(loginUsuarioDTO.Email) || string.IsNullOrEmpty(loginUsuarioDTO.Senha))
            {
                return ResultadoOperacao.Falhou("Campo de login ou senha vazios", TipoErroOperacaoEnum.NaoEncontrado);
            }

            var usuario = await _usuarioRepository.BuscaUsuarioPorEmail(loginUsuarioDTO.Email);

            if (usuario is null)
            {
                return ResultadoOperacao.Falhou("Nenhum usuário encontrado", TipoErroOperacaoEnum.NaoEncontrado);
            }

            var usuarioValido = await ValidaCredenciais(loginUsuarioDTO);

            if (!usuarioValido)
            {
                return ResultadoOperacao.Falhou("Email ou Senha Incorretos", TipoErroOperacaoEnum.NaoAutorizado);
            }

            var token = await GeraTokenJwt(loginUsuarioDTO.Email);

            return ResultadoOperacao.Concluido("Autenticação bem sucedida", TipoErroOperacaoEnum.Nenhum, token);
        }

        public async Task<bool> ValidaCredenciais(LoginUsuarioDTO loginUsuarioDTO)
        {
            if (string.IsNullOrWhiteSpace(loginUsuarioDTO.Email) || string.IsNullOrWhiteSpace(loginUsuarioDTO.Senha))
            {
                return false;
            }

            var usuario = await _usuarioRepository.BuscaUsuarioPorEmail(loginUsuarioDTO.Email);

            if (usuario is null)
            {
                return false;
            }

            return await VerificaSenha(loginUsuarioDTO.Senha, usuario.Senha);
        }

        public async Task<bool> VerificaSenha(string senha, string hashSenha)
        {
            if (string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(hashSenha))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(senha, hashSenha);
        }
    }
}
