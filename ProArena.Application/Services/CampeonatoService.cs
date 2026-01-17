using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly IMapper _mapper;
        public CampeonatoService(ICampeonatoRepository campeonatoRepository, IMapper mapper)
        {
            _campeonatoRepository = campeonatoRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> AdicionaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO)
        {
            try
            {
                var campeonato = _mapper.Map<Campeonato>(registraCampeonatoDTO);

                if (campeonato is null)
                {
                    return ResultadoOperacao.Falhou("Erro ao mapear o campeonato.");
                }

                 await _campeonatoRepository.AdicionaCampeonato(campeonato);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message);
            }

            return ResultadoOperacao.Concluido("Campeonato adicionado com sucesso.");
        }

        public async Task<ResultadoOperacao> BuscaCampeonatoPorId(int id)
        {
            var campeonato = await _campeonatoRepository.BuscaCampeonatoPorId(id);

            if (campeonato is null)
            {
                return ResultadoOperacao.Falhou("Campeonato não encontrado.");
            }

            return ResultadoOperacao.Concluido("Campeonato encontrado com sucesso.", campeonato);
        }

        public async Task<ResultadoOperacao> BuscaTodosCampeonatos()
        {
            var campeonatos = await _campeonatoRepository.BuscaTodosCampeonatos();

            if (campeonatos.Count == 0)
            {
                return ResultadoOperacao.Falhou("Nenhum campeonato encontrado.");
            }

            return ResultadoOperacao.Concluido("Campeonatos encontrados com sucesso.", campeonatos);
        }
    }
}
