using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProArena.Application.DTOs.Usuarios;
using System.Net;
using System.Net.Http.Json;

namespace ProArena.Tests.Application
{
    public class AuthUsuarioTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthUsuarioTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("joao.dss@gmail.com","string123")]
        [InlineData("joao.silveira@gmail.com", "412415")]
        [InlineData("joao.643@gmail.com", "7567")]
        public async Task LoginDeveFalharQuandoEmailOuSenhaInvalidos(string email, string senha)
        {
            // Arrange
            var dto = new LoginUsuarioDTO
            {
                Email = email,
                Senha = senha
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login-usuario", dto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task LoginDeveFuncionarQuandoEmailESenhaValidos()
        {
            var dto = new LoginUsuarioDTO
            {
                Email = "joao.silveira@gmail.com",
                Senha = "string123"
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/auth/login-usuario",
                dto
            );

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task LoginDeveFalharQuandoEmailESenhaNulos()
        {
            var dto = new LoginUsuarioDTO
            {
                Email = null!,
                Senha = null!
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/auth/login-usuario",
                dto
            );

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
