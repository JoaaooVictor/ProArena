import { api } from './api'
import { LoginRequest } from '../interfaces/ILogin'
import { ResultadoOperacao } from '../interfaces/IResultadoOperacao'
const PATH_AUTH = '/auth/login-usuario';

export function LoginUsuario(loginRequest: LoginRequest){
    return api.post<ResultadoOperacao<string>>(PATH_AUTH, loginRequest);
}