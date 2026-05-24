import {
  IEquipe,
  IRegistraEquipe,
  IAtualizaEquipe
} from '../interfaces/IEquipe';
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao';
import { api } from './api';

const PATH = '/equipe';

export function BuscaTodasEquipes() {
  return api.get<IResultadoOperacao<IEquipe[]>>(`${PATH}/busca-todos`);
}

export function BuscaEquipePorId(id: number) {
  return api.get<IResultadoOperacao<IEquipe>>(`${PATH}/busca-por-id?id=${id}`);
}

export function RegistraEquipe(equipe: IRegistraEquipe) {
  return api.post<IResultadoOperacao<IEquipe>>(`${PATH}/registra`, equipe);
}

export function AtualizaEquipe(equipe: IAtualizaEquipe) {
  return api.put<IResultadoOperacao<IEquipe>>(`${PATH}/atualiza`, equipe);
}

export function RemoveEquipe(id: number) {
  return api.delete<IResultadoOperacao<string>>(`${PATH}/remove?id=${id}`);
}
