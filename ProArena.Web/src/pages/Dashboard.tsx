import { CardDashboard } from '../components/CardDashboard/CardDashboard';
import '../styles/dashboard.css';

export default function Dashboard() {
  return (
    <div className="container-dashboard">
      <div className="dash-top">
        <div>
          <h1 className="dash-title">Dashboard</h1>
          <p className="dash-subtitle">Visão geral do seu gerenciamento de futevôlei</p>
        </div>

        <button className="btn btn-outline-primary dash-btn">
          <i className="fa-solid fa-plus" aria-hidden="true"></i>
          <span>Novo Campeonato</span>
        </button>
      </div>

      <section className="dash-cards">
        <CardDashboard icone="fa-regular fa-user" titulo="Jogadores" quantidade={10} />
        <CardDashboard icone="fa-solid fa-trophy" titulo="Campeonatos" quantidade={10} />
        <CardDashboard icone="fa-solid fa-volleyball" titulo="Eventos" quantidade={10} />
        <CardDashboard icone="fa-solid fa-people-group" titulo="Equipes" quantidade={10} />
      </section>
    </div>
  );
}