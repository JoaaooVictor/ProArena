export enum TipoErroOperacao {
    Nenhum = 0,
    ErroAoValidar = 1,
    NaoEncontrado = 2,
    NaoAutorizado = 3,
    Mapeamento = 4,
    Inesperado = 99
}

export interface IResultadoOperacao<T> {
    mensagem: string,
    erro: boolean,
    objeto: T,
    tipoErro: TipoErroOperacao
}