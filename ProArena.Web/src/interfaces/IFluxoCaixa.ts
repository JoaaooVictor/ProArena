export interface IMovimentacaoFinanceira {
    movimentacaoFinanceiraId: number;
    descricao: string;
    valor: number;
    data: string;
    tipo: number;
    categoria?: string;
    campeonatoId?: number;
    campeonato?: { nome: string };
}

export interface IRegistraMovimentacao {
    Descricao: string;
    Valor: number;
    Data: string;
    Tipo: number;
    Categoria?: string;
    CampeonatoId?: number;
}

export interface IAtualizaMovimentacao extends IRegistraMovimentacao {
    MovimentacaoFinanceiraId: number;
}

export interface IResumoFluxoCaixa {
    totalEntradas: number;
    totalSaidas: number;
    saldo: number;
    quantidadeMovimentacoes: number;
}

export enum TipoMovimentacaoFinanceira {
    Entrada = 1,
    Saida = 2
}
