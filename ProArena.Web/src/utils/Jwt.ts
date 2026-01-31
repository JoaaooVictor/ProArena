import { IUsuarioToken } from "interfaces/IUsuario"
import { jwtDecode } from "jwt-decode"

export function BuscaUsuarioPorToken(): IUsuarioToken | null {
  const token = localStorage.getItem('token')
  if (!token) return null

  return jwtDecode<IUsuarioToken>(token)
}
