using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IMapper _mapper;

        public JogadorService(IJogadorRepository jogadorRepository, IMapper mapper)
        {
            _jogadorRepository = jogadorRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> AtualizaJogador(AtualizaJogadorDTO atualizaJogadorDTO)
        {
            var jogador = await _jogadorRepository.BuscaJogadorPorId(atualizaJogadorDTO.JogadorId);

            if (jogador is null)
            {
                return ResultadoOperacao.Falhou("Jogador não encontrado", TipoErroOperacaoEnum.NaoEncontrado);
            }

            try
            {
                _mapper.Map(atualizaJogadorDTO, jogador);
                await _jogadorRepository.AtualizaJogador(jogador);
            }
            catch (Exception)
            {
                return ResultadoOperacao.Falhou("Erro ao atualizar jogador.", TipoErroOperacaoEnum.Inesperado);
            }

            return ResultadoOperacao.Concluido("Jogador atualizado com sucesso!", TipoErroOperacaoEnum.Nenhum);
        }

        public async Task<ResultadoOperacao> BuscaJogadorPorId(int id)
        {
            var jogador = await _jogadorRepository.BuscaJogadorPorId(id);

            if (jogador is null)
            {
                return ResultadoOperacao.Concluido("Jogador não encontrado.", TipoErroOperacaoEnum.Nenhum);
            }

            return ResultadoOperacao.Concluido("Jogador encontrado com sucesso.", TipoErroOperacaoEnum.Nenhum, jogador);
        }

        public async Task<ResultadoOperacao> RegistraJogador(RegistraJogadorDTO registraJogadorDTO)
        {
            try
            {
                var jogador = _mapper.Map<Jogador>(registraJogadorDTO);
                await _jogadorRepository.RegistraJogador(jogador);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }

            return ResultadoOperacao.Concluido("Jogador adicionado com sucesso.", TipoErroOperacaoEnum.Nenhum);
        }
    }
}
