using AutoMapper;
using Moq;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Services;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Tests.Services
{
    public class JogadorServicesTests
    {
        private readonly Mock<IJogadorRepository> _jogadorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IJogadorService _jogadorService;

        public JogadorServicesTests()
        {
            _jogadorRepositoryMock = new Mock<IJogadorRepository>();
            _mapperMock = new Mock<IMapper>();

            _jogadorService = new JogadorService(
                _jogadorRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task DeveRetornarSucessoQuandoJogadorEncontrado()
        {
            // Arrange
            _jogadorRepositoryMock
                .Setup(r => r.BuscaJogadorPorId(1))
                .ReturnsAsync(new Jogador
                {
                    JogadorId = 1,
                    Nome = "João Victor"
                });

            // Act
            var response = await _jogadorService.BuscaJogadorPorId(1);

            // Assert
            Assert.False(response.Erro);
            Assert.Equal(TipoErroOperacaoEnum.Nenhum, response.TipoErro);
            Assert.NotNull(response.Objeto);
        }

        [Fact]
        public async Task DeveRetornarSucessoQuandoJogadorNaoEncontrado()
        {
            // Arrange
            _jogadorRepositoryMock
                .Setup(r => r.BuscaJogadorPorId(It.IsAny<int>()))
                .ReturnsAsync((Jogador?)null);

            // Act
            var response = await _jogadorService.BuscaJogadorPorId(999);

            // Assert
            Assert.False(response.Erro);
            Assert.Equal(TipoErroOperacaoEnum.Nenhum, response.TipoErro);
            Assert.Null(response.Objeto);
        }
    }
}
