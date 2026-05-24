import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CardDashboard } from '../components/CardDashboard/CardDashboard';
import { ModalCampeonato } from '../components/Modal/ModalCampeonato';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import { BuscaTodosJogadores } from '../services/JogadorService';
import { BuscaResumoFluxoCaixa } from '../services/FluxoCaixaService';
import { FormataMoeda } from '../utils/Formatacao';
import '../styles/dashboard.css';

export default function Dashboard() {
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [totalJogadores, setTotalJogadores] = useState(0);
  const [totalCampeonatos, setTotalCampeonatos] = useState(0);
  const [saldoCaixa, setSaldoCaixa] = useState(0);
  const [totalMovimentacoes, setTotalMovimentacoes] = useState(0);

  async function carregarResumo() {
    try {
      const [jogadoresRes, campeonatosRes, fluxoRes] = await Promise.all([
        BuscaTodosJogadores(),
        BuscaTodosCampeonatos(),
        BuscaResumoFluxoCaixa(),
      ]);

      if (!jogadoresRes.data.erro) {
        setTotalJogadores(jogadoresRes.data.objeto?.length ?? 0);
      }

      if (!campeonatosRes.data.erro) {
        setTotalCampeonatos(campeonatosRes.data.objeto?.length ?? 0);
      }

      if (!fluxoRes.data.erro && fluxoRes.data.objeto) {
        setSaldoCaixa(fluxoRes.data.objeto.saldo);
        setTotalMovimentacoes(fluxoRes.data.objeto.quantidadeMovimentacoes);
      }
    } catch {
      // Mantém zeros se a API não estiver disponível
    }
  }

  useEffect(() => {
    carregarResumo();
  }, []);

  return (
    <section className="container-dashboard">
      <header className="dash-top">
        <section>
          <h1 className="dash-title">Dashboard</h1>
          <p className="dash-subtitle">Visão geral do seu gerenciamento de futevôlei</p>
        </section>

        <section className="d-flex gap-2 flex-wrap">
          <button className="btn btn-outline-primary dash-btn" onClick={() => navigate('/jogadores')}>
            <i className="fa-solid fa-user-plus" />
            <span>Novo Jogador</span>
          </button>
          <button className="btn btn-outline-primary dash-btn" onClick={() => setIsModalOpen(true)}>
            <i className="fa-solid fa-plus" />
            <span>Novo Campeonato</span>
          </button>
        </section>
      </header>

      <section className="dash-cards">
        <CardDashboard icone="fa-regular fa-user" titulo="Jogadores" quantidade={totalJogadores} />
        <CardDashboard icone="fa-solid fa-trophy" titulo="Campeonatos" quantidade={totalCampeonatos} />
        <CardDashboard
          icone="fa-solid fa-wallet"
          titulo="Saldo da Quadra"
          quantidade={saldoCaixa}
          variacaoTexto={FormataMoeda(saldoCaixa)}
          variacaoPositiva={saldoCaixa >= 0}
        />
        <CardDashboard icone="fa-solid fa-chart-line" titulo="Movimentações" quantidade={totalMovimentacoes} />
      </section>

      <ModalCampeonato
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSuccess={carregarResumo}
      />
    </section>
  );
}
