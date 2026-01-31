import dashboardImg from '../assets/dashboard-futvolei.png'
import '../styles/dashboard.css'

export default function Dashboard() {
  return (
    <div className="container-fluid pt-4 pb-5 fundo-imagem">
      <img 
        src={dashboardImg} 
        alt="Dashboard Futvôlei" 
        className="imagem-dashboard rounded-4" 
      />

      <div className="texto-overlay">
        <h1>Reserve sua quadra</h1>
        <p>Encontre seu jogo</p>
        <h4>Descubra as quadras esportivas de alto nível e participe de campeonatos em sua cidade.</h4>
      </div>
    </div>
  )
}
