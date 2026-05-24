import { IJogador } from './IJogador';

export interface IEquipe {
  equipeId: number;
  nome: string;
  nomeExibicao: string;
  campeonatoId?: number;
  jogadores: IJogador[];
}

export interface IRegistraEquipe {
  Nome: string;
  JogadorIds: number[];
  CampeonatoId: number;
}

export interface IAtualizaEquipe {
  EquipeId: number;
  Nome: string;
  JogadorIds: number[];
}
