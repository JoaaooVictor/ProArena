import {
    IAtualizaMovimentacao,
    IMovimentacaoFinanceira,
    IRegistraMovimentacao,
    IResumoFluxoCaixa
} from '../interfaces/IFluxoCaixa';
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao';
import { api } from './api';

const PATH = '/fluxo-caixa';

export function BuscaTodasMovimentacoes() {
    return api.get<IResultadoOperacao<IMovimentacaoFinanceira[]>>(`${PATH}/busca-todos`);
}

export function BuscaResumoFluxoCaixa(inicio?: string, fim?: string) {
    const params = new URLSearchParams();
    if (inicio) params.append('inicio', inicio);
    if (fim) params.append('fim', fim);
    const query = params.toString();
    return api.get<IResultadoOperacao<IResumoFluxoCaixa>>(`${PATH}/resumo${query ? `?${query}` : ''}`);
}

export function RegistraMovimentacao(movimentacao: IRegistraMovimentacao) {
    return api.post<IResultadoOperacao<IMovimentacaoFinanceira>>(`${PATH}/registra`, movimentacao);
}

export function AtualizaMovimentacao(movimentacao: IAtualizaMovimentacao) {
    return api.put<IResultadoOperacao<IMovimentacaoFinanceira>>(`${PATH}/atualiza`, movimentacao);
}

export function RemoveMovimentacao(id: number) {
    return api.delete<IResultadoOperacao<string>>(`${PATH}/remove?id=${id}`);
}
