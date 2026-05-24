import { IEquipe } from './IEquipe';

export interface IPartida {
  partidaId: number;
  dataHora: string;
  campeonatoId: number;
  equipes: IEquipe[];
}

export interface IRegistraPartida {
  DataHora: string;
  CampeonatoId: number;
  EquipeIds: number[];
}

export interface IAtualizaPartida {
  PartidaId: number;
  DataHora: string;
  EquipeIds: number[];
}
