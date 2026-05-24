export interface IJogador {
    jogadorId: number;
    nome: string;
    cpf: string;
    idade: number;
    ativo: boolean;
}

export interface IRegistraJogador {
    Nome: string;
    Cpf: string;
    Idade: number;
}

export interface IAtualizaJogador {
    JogadorId: number;
    Nome: string;
    Idade: number;
    Ativo: boolean;
}
