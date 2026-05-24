import { NavLink, useNavigate } from "react-router-dom";
import { BuscaUsuarioPorToken } from "../../utils/Jwt";
import { Logout } from "../../services/AuthService";

export default function Sidebar() {
  const user = BuscaUsuarioPorToken();
  const navigate = useNavigate();

  function GerenciaLogout() {
    Logout();
    navigate("/login", { replace: true });
  }

  return (
    <aside className="sidebar">
      <div className="brand">
        <div className="logo" aria-hidden="true">
          <i className="fa-solid fa-volleyball"></i>
        </div>

        <div>
          <h3>ProArena</h3>
          <small>Gerenciamento</small>
        </div>
      </div>

      <div className="menu-title">MENU</div>

      <nav className="menu">
        <NavLink to="/dashboard" className={({ isActive }) => (isActive ? "active" : "")}>
          <i className="fa-solid fa-chart-pie"></i>
          <span>Dashboard</span>
        </NavLink>

        <NavLink to="/jogadores" className={({ isActive }) => (isActive ? "active" : "")}>
          <i className="fa-regular fa-user"></i>
          <span>Jogadores</span>
        </NavLink>

        <NavLink to="/campeonatos" className={({ isActive }) => (isActive ? "active" : "")}>
          <i className="fa-solid fa-trophy"></i>
          <span>Campeonatos</span>
        </NavLink>

        <NavLink to="/fluxo-caixa" className={({ isActive }) => (isActive ? "active" : "")}>
          <i className="fa-solid fa-wallet"></i>
          <span>Fluxo de Caixa</span>
        </NavLink>
      </nav>

      <div className="user-box">
        <div className="avatar" aria-hidden="true"><i className="fa-solid fa-user"></i></div>
        <div className="user-info">
          <strong>{user?.nome ?? "Usuário"}</strong>
          <small>{user?.email ?? ""}</small>
        </div>
        <button className="logout-btn" onClick={GerenciaLogout} title="Sair">
            <i className="fa-solid fa-right-from-bracket" aria-hidden="true"></i>
        </button>
      </div>

    </aside>
  );
}
