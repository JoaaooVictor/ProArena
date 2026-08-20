using AutoMapper;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Application.DTOs.Campeonatos;

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

        public async Task<ResultadoOperacao> RegistraResultado(RegistraResultadoPartidaDTO dto)
        {
            var partida = await _partidaRepository.BuscaPorId(dto.PartidaId);

            if (partida is null)
            {
                return ResultadoOperacao.Falhou("Partida não encontrada", TipoErroOperacaoEnum.NaoEncontrado);
            }

            // Verificar se a equipe vencedora está na partida
            var equipeVencedora = partida.Equipes.FirstOrDefault(e => e.EquipeId == dto.VencedorId);
            if (equipeVencedora is null)
            {
                return ResultadoOperacao.Falhou("A equipe vencedora não está participando desta partida", TipoErroOperacaoEnum.Validacao);
            }

            try
            {
                partida.VencedorId = dto.VencedorId;
                partida.Concluido = true;
                
                await _partidaRepository.AtualizaPartida(partida);
                
                // Verificar se todas as partidas da rodada atual foram concluídas
                var todasPartidasCampeonato = await _partidaRepository.BuscaPorCampeonato(partida.CampeonatoId);
                var rodadaAtual = (partida.DataHora - DateTime.Now).Days + 1;
                var partidasRodadaAtual = todasPartidasCampeonato
                    .Where(p => (p.DataHora - DateTime.Now).Days + 1 == rodadaAtual)
                    .ToList();
                
                var todasConcluidas = partidasRodadaAtual.All(p => p.Concluido);
                
                if (todasConcluidas && partidasRodadaAtual.Count > 1)
                {
                    // Gerar partidas da próxima rodada com os vencedores
                    var vencedores = partidasRodadaAtual.Select(p => p.VencedorId!.Value).ToList();
                    var equipesVencedoras = new List<Equipe>();
                    
                    foreach (var vencedorId in vencedores)
                    {
                        var equipe = await _equipeRepository.BuscaPorId(vencedorId);
                        if (equipe != null)
                        {
                            equipesVencedoras.Add(equipe);
                        }
                    }
                    
                    // Gerar novas partidas
                    for (int i = 0; i < equipesVencedoras.Count; i += 2)
                    {
                        if (i + 1 < equipesVencedoras.Count)
                        {
                            var novaPartida = new Partida
                            {
                                DataHora = DateTime.Now.AddDays(rodadaAtual + 1),
                                CampeonatoId = partida.CampeonatoId,
                                Equipes = new List<Equipe> { equipesVencedoras[i], equipesVencedoras[i + 1] }
                            };
                            await _partidaRepository.RegistraPartida(novaPartida);
                        }
                    }
                }
                
                return ResultadoOperacao.Concluido("Resultado registrado com sucesso!", TipoErroOperacaoEnum.Nenhum, partida);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou($"Erro ao registrar resultado: {ex.Message}", TipoErroOperacaoEnum.Inesperado);
            }
        }
    }
}
