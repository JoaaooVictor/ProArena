import { api } from './api'
import { ILoginRequest } from '../interfaces/ILogin'
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao'
const PATH_AUTH = '/auth/login-usuario';

export function LoginUsuario(loginRequest: ILoginRequest){
    return api.post<IResultadoOperacao<string>>(PATH_AUTH, loginRequest);
}