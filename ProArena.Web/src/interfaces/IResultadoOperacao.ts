export interface ResultadoOperacao<T> {
    mensagem: string,
    erro: boolean,
    objeto: T
}