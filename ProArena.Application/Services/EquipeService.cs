using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Domain.Enums;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IMapper _mapper;

        public EquipeService(IEquipeRepository equipeRepository, IJogadorRepository jogadorRepository, IMapper mapper)
        {
            _equipeRepository = equipeRepository;
            _jogadorRepository = jogadorRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> RegistraEquipe(RegistraEquipeDTO dto)
        {
            try
            {
                var equipe = _mapper.Map<Equipe>(dto);
                
                // Buscar jogadores pelos IDs
                var jogadores = new List<Jogador>();
                foreach (var jogadorId in dto.JogadorIds)
                {
                    var jogador = await _jogadorRepository.BuscaJogadorPorId(jogadorId);
                    if (jogador != null)
                    {
                        jogadores.Add(jogador);
                    }
                }
                
                equipe.Jogadores = jogadores;
                
                await _equipeRepository.RegistraEquipe(equipe);
                
                // Nota: A inscrição será criada automaticamente pelo banco de dados
                // se houver um relacionamento configurado, ou pode ser implementada
                // em um endpoint separado para gerenciar inscrições
                
                return ResultadoOperacao.Concluido("Equipe registrada com sucesso!", TipoErroOperacaoEnum.Nenhum, equipe);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> AtualizaEquipe(AtualizaEquipeDTO dto)
        {
            var equipe = await _equipeRepository.BuscaPorId(dto.EquipeId);

            if (equipe is null)
            {
                return ResultadoOperacao.Falhou("Equipe não encontrada", TipoErroOperacaoEnum.NaoEncontrado);
            }

            try
            {
                equipe.Nome = dto.Nome;
                
                // Atualizar jogadores
                var jogadores = new List<Jogador>();
                foreach (var jogadorId in dto.JogadorIds)
                {
                    var jogador = await _jogadorRepository.BuscaJogadorPorId(jogadorId);
                    if (jogador != null)
                    {
                        jogadores.Add(jogador);
                    }
                }
                
                equipe.Jogadores = jogadores;
                
                await _equipeRepository.AtualizaEquipe(equipe);
            }
            catch (Exception)
            {
                return ResultadoOperacao.Falhou("Erro ao atualizar equipe.", TipoErroOperacaoEnum.Inesperado);
            }

            return ResultadoOperacao.Concluido("Equipe atualizada com sucesso!", TipoErroOperacaoEnum.Nenhum);
        }

        public async Task<ResultadoOperacao> BuscaPorId(int id)
        {
            var equipe = await _equipeRepository.BuscaPorId(id);

            if (equipe is null)
            {
                return ResultadoOperacao.Falhou("Equipe não encontrada.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            // Mapear para DTO para evitar referência circular
            var equipeDto = new EquipeDTO
            {
                EquipeId = equipe.EquipeId,
                Nome = equipe.Nome,
                NomeExibicao = equipe.NomeExibicao,
                CampeonatoId = equipe.CampeonatoId,
                Jogadores = equipe.Jogadores.Select(j => new JogadorDTO
                {
                    JogadorId = j.JogadorId,
                    Nome = j.Nome,
                    Cpf = j.Cpf,
                    Idade = j.Idade,
                    Ativo = j.Ativo
                }).ToList()
            };

            return ResultadoOperacao.Concluido("Equipe encontrada com sucesso.", TipoErroOperacaoEnum.Nenhum, equipeDto);
        }

        public async Task<ResultadoOperacao> BuscaTodos()
        {
            try
            {
                var equipes = await _equipeRepository.BuscaTodos();
                
                // Mapear para DTOs para evitar referência circular
                var equipesDto = equipes.Select(e => new EquipeDTO
                {
                    EquipeId = e.EquipeId,
                    Nome = e.Nome,
                    NomeExibicao = e.NomeExibicao,
                    CampeonatoId = e.CampeonatoId,
                    Jogadores = e.Jogadores.Select(j => new JogadorDTO
                    {
                        JogadorId = j.JogadorId,
                        Nome = j.Nome,
                        Cpf = j.Cpf,
                        Idade = j.Idade,
                        Ativo = j.Ativo
                    }).ToList()
                }).ToList();
                
                return ResultadoOperacao.Concluido("Equipes encontradas com sucesso.", TipoErroOperacaoEnum.Nenhum, equipesDto);
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
                await _equipeRepository.Remove(id);
                return ResultadoOperacao.Concluido("Equipe removida com sucesso.", TipoErroOperacaoEnum.Nenhum);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }
    }
}
