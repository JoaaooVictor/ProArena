import { BuscaUsuarioPorToken } from '../utils/Jwt'

export default function Sidebar() {
  const user = BuscaUsuarioPorToken()

  return (
    <div className="sidebar">
      <h3>Arena Admin</h3>

      <nav>
        <a>Dashboard</a>
        <a>Times</a>
        <a>Partidas</a>
      </nav>

      <div className="user-box">
        <strong>{user?.nome}</strong>
        <small>{user?.email}</small>
      </div>
    </div>
  )
}
