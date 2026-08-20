import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CardDashboard } from '../components/CardDashboard/CardDashboard';
import { ModalCampeonato } from '../components/Modal/ModalCampeonato';
import Bracket from '../components/Bracket/Bracket';
import { BuscaTodosCampeonatos } from '../services/CampeonatoService';
import { BuscaTodosJogadores } from '../services/JogadorService';
import { BuscaResumoFluxoCaixa } from '../services/FluxoCaixaService';
import { BuscaPartidasPorCampeonato } from '../services/PartidaService';
import { BuscaChaveamento } from '../services/ChaveamentoService';
import { FormataMoeda, FormataData } from '../utils/Formatacao';
import { IPartida } from '../interfaces/IPartida';
import { ICampeonato } from '../interfaces/ICampeonato';
import { IChaveamento } from '../interfaces/IChaveamento';
import '../styles/dashboard.css';
import '../styles/bracket.css';

export default function Dashboard() {
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [totalJogadores, setTotalJogadores] = useState(0);
  const [totalCampeonatos, setTotalCampeonatos] = useState(0);
  const [saldoCaixa, setSaldoCaixa] = useState(0);
  const [totalMovimentacoes, setTotalMovimentacoes] = useState(0);
  const [partidas, setPartidas] = useState<IPartida[]>([]);
  const [campeonatos, setCampeonatos] = useState<ICampeonato[]>([]);
  const [chaveamento, setChaveamento] = useState<IChaveamento | null>(null);

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
        setCampeonatos(campeonatosRes.data.objeto ?? []);
      }

      if (!fluxoRes.data.erro && fluxoRes.data.objeto) {
        setSaldoCaixa(fluxoRes.data.objeto.saldo);
        setTotalMovimentacoes(fluxoRes.data.objeto.quantidadeMovimentacoes);
      }
    } catch {
      
    }
  }

  async function carregarPartidas() {
    try {
      const campeonatoAtivo = campeonatos[0];
      if (campeonatoAtivo) {
        const response = await BuscaPartidasPorCampeonato(campeonatoAtivo.campeonatoId);
        if (!response.data.erro) {
          setPartidas(response.data.objeto ?? []);
        }
        
        const chaveamentoResponse = await BuscaChaveamento(campeonatoAtivo.campeonatoId);
        if (!chaveamentoResponse.data.erro) {
          setChaveamento(chaveamentoResponse.data.objeto);
        }
      }
    } catch {
    }
  }

  useEffect(() => {
    carregarResumo();
  }, []);

  useEffect(() => {
    if (campeonatos.length > 0) {
      carregarPartidas();
    }
  }, [campeonatos]);

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

      {partidas.length > 0 && (
        <section className="partidas-section">
          <h2 className="section-title">Partidas - {campeonatos[0]?.nome}</h2>
          <div className="partidas-hierarchy">
            {partidas.map((partida) => (
              <div key={partida.partidaId} className="partida-card">
                <div className="partida-date">{FormataData(partida.dataHora)}</div>
                <div className="partida-teams">
                  {partida.equipes.map((equipe) => (
                    <div key={equipe.equipeId} className="team-badge">
                      {equipe.nome}
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </div>
        </section>
      )}

      {chaveamento && (
        <section className="bracket-section">
          <Bracket chaveamento={chaveamento} onChaveamentoUpdate={carregarPartidas} />
        </section>
      )}

      <ModalCampeonato
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSuccess={carregarResumo}
      />
    </section>
  );
}
