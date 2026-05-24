export interface ICampeonato {
    campeonatoId: number;
    nome: string;
    descricao?: string;
    dataInicio?: string;
    dataFim?: string;
    dataInicioInscricao?: string;
    dataFimInscricao?: string;
    ativo: boolean;
}

export interface ICriaCampeonato {
    Nome: string;
    Descricao: string;
    DataInicio: string;
    DataFim: string;
    DataInicioInscricao: string;
    DataFimInscricao: string;
}
