using AutoMapper;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Application.DTOs.Campeonatos;
using ProArena.Application.DTOs.Equipes;

namespace ProArena.Application.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly IPartidaRepository _partidaRepository;
        private readonly IMapper _mapper;

        public CampeonatoService(ICampeonatoRepository campeonatoRepository, IEquipeRepository equipeRepository, IPartidaRepository partidaRepository, IMapper mapper)
        {
            _campeonatoRepository = campeonatoRepository;
            _equipeRepository = equipeRepository;
            _partidaRepository = partidaRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> AdicionaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO)
        {
            try
            {
                var campeonato = _mapper.Map<Campeonato>(registraCampeonatoDTO);
                campeonato.Ativo = true;

                if (registraCampeonatoDTO.Equipes is not null && registraCampeonatoDTO.Equipes.Count > 0)
                {
                    foreach (var equipe in registraCampeonatoDTO.Equipes)
                    {
                        var equipeEntity = _mapper.Map<Equipe>(equipe);
                        campeonato.Inscricoes.Add(new Inscricao { Equipe = equipeEntity });
                    }
                }

                await _campeonatoRepository.AdicionaCampeonato(campeonato);
                return ResultadoOperacao.Concluido("Campeonato adicionado com sucesso.", TipoErroOperacaoEnum.Nenhum, campeonato);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaCampeonatoPorId(int id)
        {
            var campeonato = await _campeonatoRepository.BuscaCampeonatoPorId(id);

            if (campeonato is null)
            {
                return ResultadoOperacao.Falhou("Campeonato não encontrado.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            return ResultadoOperacao.Concluido("Campeonato encontrado com sucesso.", TipoErroOperacaoEnum.Nenhum, campeonato);
        }

        public async Task<ResultadoOperacao> BuscaTodosCampeonatos()
        {
            try
            {
                var campeonatos = await _campeonatoRepository.BuscaTodosCampeonatos();
                return ResultadoOperacao.Concluido("Campeonatos encontrados com sucesso.", TipoErroOperacaoEnum.Nenhum, campeonatos);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> IniciaCampeonato(int campeonatoId)
        {
            try
            {
                var campeonato = await _campeonatoRepository.BuscaCampeonatoPorId(campeonatoId);
                if (campeonato is null)
                {
                    return ResultadoOperacao.Falhou("Campeonato não encontrado.", TipoErroOperacaoEnum.NaoEncontrado);
                }

                // Buscar equipes inscritas no campeonato
                var equipes = await _equipeRepository.BuscaPorCampeonato(campeonatoId);
                
                // Log para debug
                Console.WriteLine($"Campeonato ID: {campeonatoId}");
                Console.WriteLine($"Equipes encontradas: {equipes.Count}");
                foreach (var equipe in equipes)
                {
                    Console.WriteLine($"  - Equipe ID: {equipe.EquipeId}, Nome: {equipe.Nome}, CampeonatoId: {equipe.CampeonatoId}");
                }
                
                if (equipes.Count < 2)
                {
                    return ResultadoOperacao.Falhou($"É necessário pelo menos 2 equipes para iniciar o campeonato. Encontradas: {equipes.Count}", TipoErroOperacaoEnum.Validacao);
                }

                // Gerar chaves (bracket) - sistema de eliminação simples
                var partidasGeradas = new List<Partida>();
                var equipesEmbaralhadas = equipes.OrderBy(e => Guid.NewGuid()).ToList();
                var numeroRodada = 1;

                // Gerar partidas da primeira rodada
                for (int i = 0; i < equipesEmbaralhadas.Count; i += 2)
                {
                    if (i + 1 < equipesEmbaralhadas.Count)
                    {
                        var partida = new Partida
                        {
                            DataHora = DateTime.Now.AddDays(numeroRodada),
                            CampeonatoId = campeonatoId,
                            Equipes = new List<Equipe> { equipesEmbaralhadas[i], equipesEmbaralhadas[i + 1] }
                        };
                        partidasGeradas.Add(partida);
                    }
                }

                Console.WriteLine($"Partidas geradas: {partidasGeradas.Count}");

                // Salvar todas as partidas
                foreach (var partida in partidasGeradas)
                {
                    await _partidaRepository.RegistraPartida(partida);
                }

                return ResultadoOperacao.Concluido($"Campeonato iniciado com sucesso! {partidasGeradas.Count} partidas geradas.", TipoErroOperacaoEnum.Nenhum, partidasGeradas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar campeonato: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return ResultadoOperacao.Falhou($"Erro ao iniciar campeonato: {ex.Message}", TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaChaveamento(int campeonatoId)
        {
            try
            {
                var campeonato = await _campeonatoRepository.BuscaCampeonatoPorId(campeonatoId);
                if (campeonato is null)
                {
                    return ResultadoOperacao.Falhou("Campeonato não encontrado.", TipoErroOperacaoEnum.NaoEncontrado);
                }

                var partidas = await _partidaRepository.BuscaPorCampeonato(campeonatoId);
                if (!partidas.Any())
                {
                    // Retornar chaveamento vazio em vez de erro
                    var chaveamentoVazio = new ChaveamentoDTO
                    {
                        CampeonatoId = campeonatoId,
                        CampeonatoNome = campeonato.Nome,
                        Rodadas = new List<RodadaDTO>()
                    };
                    return ResultadoOperacao.Concluido("Nenhuma partida encontrada. O campeonato precisa ser iniciado para gerar o chaveamento.", TipoErroOperacaoEnum.Nenhum, chaveamentoVazio);
                }

                // Agrupar partidas por rodada (baseado na data)
                var chaveamento = new ChaveamentoDTO
                {
                    CampeonatoId = campeonatoId,
                    CampeonatoNome = campeonato.Nome,
                    Rodadas = new List<RodadaDTO>()
                };

                var partidasPorRodada = partidas
                    .GroupBy(p => (p.DataHora - campeonato.DataInicio!).Value.Days + 1)
                    .OrderBy(g => g.Key);

                foreach (var grupo in partidasPorRodada)
                {
                    var nomeRodada = grupo.Key switch
                    {
                        1 => "Oitavas de Final",
                        2 => "Quartas de Final",
                        3 => "Semifinais",
                        4 => "Final",
                        _ => $"Rodada {grupo.Key}"
                    };

                    var rodada = new RodadaDTO
                    {
                        NumeroRodada = grupo.Key,
                        NomeRodada = nomeRodada,
                        Jogos = grupo.Select(p => new JogoDTO
                        {
                            PartidaId = p.PartidaId,
                            DataHora = p.DataHora,
                            Equipe1 = p.Equipes.FirstOrDefault() != null ? new EquipeDTO
                            {
                                EquipeId = p.Equipes.First().EquipeId,
                                Nome = p.Equipes.First().Nome,
                                NomeExibicao = p.Equipes.First().NomeExibicao
                            } : null,
                            Equipe2 = p.Equipes.Skip(1).FirstOrDefault() != null ? new EquipeDTO
                            {
                                EquipeId = p.Equipes.Skip(1).First().EquipeId,
                                Nome = p.Equipes.Skip(1).First().Nome,
                                NomeExibicao = p.Equipes.Skip(1).First().NomeExibicao
                            } : null,
                            Concluido = false
                        }).ToList()
                    };

                    chaveamento.Rodadas.Add(rodada);
                }

                return ResultadoOperacao.Concluido("Chaveamento encontrado com sucesso.", TipoErroOperacaoEnum.Nenhum, chaveamento);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }
    }
}
