export interface ResultadoOperacaoDTO<T> {
  erro : boolean,
  mensagem: string,
  objeto: T,
}