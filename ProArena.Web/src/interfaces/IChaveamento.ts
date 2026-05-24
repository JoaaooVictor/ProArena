export interface IChaveamento {
  campeonatoId: number;
  campeonatoNome: string;
  rodadas: IRodada[];
}

export interface IRodada {
  numeroRodada: number;
  nomeRodada: string;
  jogos: IJogo[];
}

export interface IJogo {
  partidaId: number;
  dataHora: string;
  equipe1: IEquipeDTO | null;
  equipe2: IEquipeDTO | null;
  vencedorId?: number;
  concluido: boolean;
}

export interface IEquipeDTO {
  equipeId: number;
  nome: string;
  nomeExibicao: string;
}
