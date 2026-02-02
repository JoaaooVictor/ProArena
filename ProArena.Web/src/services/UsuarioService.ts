import { IRegistraUsuario } from "../interfaces/IUsuario";
import { api } from "./api";
import { IResultadoOperacao } from "../interfaces/IResultadoOperacao";
const PATH_USUARIO = '/usuario';

export function RegistraUsuario(registraUsuario : IRegistraUsuario){
    return api.post<IResultadoOperacao<string>>(`${PATH_USUARIO}/registra-usuario`, registraUsuario);
}
