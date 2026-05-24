using AutoMapper;
using ProArena.Application.DTOs;
using ProArena.Application.Interfaces;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;
using ProArena.Domain.Enums;
using ProArena.Domain.Interfaces;

namespace ProArena.Application.Services
{
    public class MovimentacaoFinanceiraService : IMovimentacaoFinanceiraService
    {
        private readonly IMovimentacaoFinanceiraRepository _repository;
        private readonly IMapper _mapper;

        public MovimentacaoFinanceiraService(IMovimentacaoFinanceiraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacao> RegistraAsync(RegistraMovimentacaoFinanceiraDTO dto)
        {
            try
            {
                var movimentacao = _mapper.Map<MovimentacaoFinanceira>(dto);
                await _repository.RegistraAsync(movimentacao);
                return ResultadoOperacao.Concluido("Movimentação registrada com sucesso.", TipoErroOperacaoEnum.Nenhum, movimentacao);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> AtualizaAsync(AtualizaMovimentacaoFinanceiraDTO dto)
        {
            var movimentacao = await _repository.BuscaPorIdAsync(dto.MovimentacaoFinanceiraId);
            if (movimentacao is null)
            {
                return ResultadoOperacao.Falhou("Movimentação não encontrada.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            try
            {
                _mapper.Map(dto, movimentacao);
                await _repository.AtualizaAsync(movimentacao);
                return ResultadoOperacao.Concluido("Movimentação atualizada com sucesso.", TipoErroOperacaoEnum.Nenhum, movimentacao);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> RemoveAsync(int id)
        {
            var movimentacao = await _repository.BuscaPorIdAsync(id);
            if (movimentacao is null)
            {
                return ResultadoOperacao.Falhou("Movimentação não encontrada.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            try
            {
                await _repository.RemoveAsync(movimentacao);
                return ResultadoOperacao.Concluido("Movimentação removida com sucesso.", TipoErroOperacaoEnum.Nenhum);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaPorIdAsync(int id)
        {
            var movimentacao = await _repository.BuscaPorIdAsync(id);
            if (movimentacao is null)
            {
                return ResultadoOperacao.Falhou("Movimentação não encontrada.", TipoErroOperacaoEnum.NaoEncontrado);
            }

            return ResultadoOperacao.Concluido("Movimentação encontrada.", TipoErroOperacaoEnum.Nenhum, movimentacao);
        }

        public async Task<ResultadoOperacao> BuscaTodosAsync()
        {
            try
            {
                var movimentacoes = await _repository.BuscaTodosAsync();
                return ResultadoOperacao.Concluido("Movimentações encontradas.", TipoErroOperacaoEnum.Nenhum, movimentacoes);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                var movimentacoes = await _repository.BuscaPorPeriodoAsync(inicio, fim);
                return ResultadoOperacao.Concluido("Movimentações encontradas.", TipoErroOperacaoEnum.Nenhum, movimentacoes);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }

        public async Task<ResultadoOperacao> BuscaResumoAsync(DateTime? inicio = null, DateTime? fim = null)
        {
            try
            {
                List<MovimentacaoFinanceira> movimentacoes;

                if (inicio.HasValue && fim.HasValue)
                {
                    movimentacoes = await _repository.BuscaPorPeriodoAsync(inicio.Value, fim.Value);
                }
                else
                {
                    movimentacoes = await _repository.BuscaTodosAsync();
                }

                var entradas = movimentacoes
                    .Where(m => m.Tipo == TipoMovimentacaoFinanceiraEnum.Entrada)
                    .Sum(m => m.Valor);

                var saidas = movimentacoes
                    .Where(m => m.Tipo == TipoMovimentacaoFinanceiraEnum.Saida)
                    .Sum(m => m.Valor);

                var resumo = new ResumoFluxoCaixaDTO
                {
                    TotalEntradas = entradas,
                    TotalSaidas = saidas,
                    Saldo = entradas - saidas,
                    QuantidadeMovimentacoes = movimentacoes.Count
                };

                return ResultadoOperacao.Concluido("Resumo gerado com sucesso.", TipoErroOperacaoEnum.Nenhum, resumo);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.Falhou(ex.Message, TipoErroOperacaoEnum.Inesperado);
            }
        }
    }
}
