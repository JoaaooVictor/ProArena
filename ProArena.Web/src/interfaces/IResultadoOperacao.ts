export interface IResultadoOperacao<T> {
    mensagem: string,
    erro: boolean,
    objeto: T
}