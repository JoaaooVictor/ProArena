using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _partidaRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly IMapper _mapper;

        public PartidaService(IPartidaRepository partidaRepository, IEquipeRepository equipeRepository, IMapper mapper)
        {
            _partidaRepository = partidaRepository;
            _equipeRepository = equipeRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> RegistraPartida(RegistraPartidaDTO dto)
        {
            try
            {
                var partida = _mapper.Map<Partida>(dto);
                
                // Buscar equipes pelos IDs
                var equipes = new List<Equipe>();
                foreach (var equipeId in dto.EquipeIds)
                {
                    var equipe = await _equipeRepository.BuscaPorId(equipeId);
                    if (equipe != null)
                    {
                        equipes.Add(equipe);
                    }
                }
                
                partida.Equipes = equipes;
                
                await _partidaRepository.RegistraPartida(partida);
                
                return ResultadoOperacao.Concluido("Partida registrada com sucesso!", TipoErroOperacaoEnum.Nenhum, partida);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> AtualizaPartida(AtualizaPartidaDTO dto)
        {
            var partida = await _partidaRepository.BuscaPorId(dto.PartidaId);

            if (partida is null)
            {
                return ResultadoOperacao.Falhou("Partida não encontrada", TipoErroOperacaoEnum.NaoEncontrado);
            }

            try
            {
                partida.DataHora = dto.DataHora;
                
                // Atualizar equipes
                var equipes = new List<Equipe>();
                foreach (var equipeId in dto.EquipeIds)
                {
                    var equipe = await _equipeRepository.BuscaPorId(equipeId);
                    if (equipe != null)
                    {
                        equipes.Add(equipe);
                    }
                }
                
                partida.Equipes = equipes;
                
                await _partidaRepository.AtualizaPartida(partida);
            }
            catch (Exception)
            {
                return ResultadoOperacao.Falhou("Erro ao atualizar partida.", TipoErroOperacaoEnum.Inesperado);
            }

            return ResultadoOperacao.Concluido("Partida atualizada com sucesso!", TipoErroOperacaoEnum.Nenhum);
        }

        public async Task<ResultadoOperacao> BuscaPorId(int id)
        {
            var partida = await _partidaRepository.BuscaPorId(id);

            if (partida is null)
            {
                return ResultadoOperacao.Falhou("Partida não encontrada.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            return ResultadoOperacao.Concluido("Partida encontrada com sucesso.", TipoErroOperacaoEnum.Nenhum, partida);
        }

        public async Task<ResultadoOperacao> BuscaPorCampeonato(int campeonatoId)
        {
            try
            {
                var partidas = await _partidaRepository.BuscaPorCampeonato(campeonatoId);
                return ResultadoOperacao.Concluido("Partidas encontradas com sucesso.", TipoErroOperacaoEnum.Nenhum, partidas);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaTodos()
        {
            try
            {
                var partidas = await _partidaRepository.BuscaTodos();
                return ResultadoOperacao.Concluido("Partidas encontradas com sucesso.", TipoErroOperacaoEnum.Nenhum, partidas);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> Remove(int id)
        {
            try
            {
                await _partidaRepository.Remove(id);
                return ResultadoOperacao.Concluido("Partida removida com sucesso.", TipoErroOperacaoEnum.Nenhum);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }
    }
}
