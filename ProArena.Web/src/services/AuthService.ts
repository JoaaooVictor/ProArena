import { api } from './api'
import { ILoginRequest } from '../interfaces/ILogin'
import { IResultadoOperacao } from '../interfaces/IResultadoOperacao'
const PATH_AUTH = '/auth/login-usuario';
const TOKEN_KEY = "token";

export function LoginUsuario(loginRequest: ILoginRequest){
    return api.post<IResultadoOperacao<string>>(PATH_AUTH, loginRequest);
}

export function Logout() {
  localStorage.removeItem(TOKEN_KEY);
}