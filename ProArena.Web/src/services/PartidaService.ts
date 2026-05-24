import {
  IPartida,
  IRegistraPartida,
  IAtualizaPartida
} from '../interfaces/IPartida';
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao';
import { api } from './api';

const PATH = '/partida';

export function BuscaTodasPartidas() {
  return api.get<IResultadoOperacao<IPartida[]>>(`${PATH}/busca-todos`);
}

export function BuscaPartidaPorId(id: number) {
  return api.get<IResultadoOperacao<IPartida>>(`${PATH}/busca-por-id?id=${id}`);
}

export function BuscaPartidasPorCampeonato(campeonatoId: number) {
  return api.get<IResultadoOperacao<IPartida[]>>(`${PATH}/busca-por-campeonato?campeonatoId=${campeonatoId}`);
}

export function RegistraPartida(partida: IRegistraPartida) {
  return api.post<IResultadoOperacao<IPartida>>(`${PATH}/registra`, partida);
}

export function AtualizaPartida(partida: IAtualizaPartida) {
  return api.put<IResultadoOperacao<IPartida>>(`${PATH}/atualiza`, partida);
}

export function RemovePartida(id: number) {
  return api.delete<IResultadoOperacao<string>>(`${PATH}/remove?id=${id}`);
}
