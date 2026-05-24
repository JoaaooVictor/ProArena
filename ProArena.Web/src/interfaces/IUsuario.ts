export interface IUsuarioToken {
  nome: string
  email: string
}

export enum TipoUsuario {
    ProArenaAdmin = 1,
    QuadraAdmin = 2,
    Jogador = 3,
}

export interface IRegistraUsuario{
    nome: string,
    cpf: string,
    email: string,
    senha: string,
    tipoUsuario: TipoUsuario
}