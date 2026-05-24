import { IAtualizaJogador, IJogador, IRegistraJogador } from '../interfaces/IJogador';
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao';
import { api } from './api';

const PATH = '/jogador';

export function BuscaTodosJogadores() {
    return api.get<IResultadoOperacao<IJogador[]>>(`${PATH}/busca-todos-jogadores`);
}

export function BuscaJogadorPorId(id: number) {
    return api.get<IResultadoOperacao<IJogador>>(`${PATH}/busca-jogador-${id}`);
}

export function RegistraJogador(jogador: IRegistraJogador) {
    return api.post<IResultadoOperacao<string>>(`${PATH}/registra-jogador`, jogador);
}

export function AtualizaJogador(jogador: IAtualizaJogador) {
    return api.put<IResultadoOperacao<string>>(`${PATH}/atualiza-jogador`, jogador);
}
