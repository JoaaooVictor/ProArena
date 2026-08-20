import { useState } from 'react';
import { IChaveamento, IRodada, IJogo } from '../../interfaces/IChaveamento';
import { RegistraResultadoPartida } from '../../services/PartidaService';

interface BracketProps {
  chaveamento: IChaveamento;
  onChaveamentoUpdate?: () => void;
}

export default function Bracket({ chaveamento, onChaveamentoUpdate }: BracketProps) {
  const [loading, setLoading] = useState(false);

  const handleSelecionarVencedor = async (partidaId: number, vencedorId: number) => {
    setLoading(true);
    try {
      await RegistraResultadoPartida({ partidaId, vencedorId });
      if (onChaveamentoUpdate) {
        onChaveamentoUpdate();
      }
    } catch (error) {
      console.error('Erro ao registrar resultado:', error);
    } finally {
      setLoading(false);
    }
  };

  if (!chaveamento.rodadas || chaveamento.rodadas.length === 0) {
    return (
      <div className="bracket-container">
        <h3 className="bracket-title">{chaveamento.campeonatoNome} - Chaveamento</h3>
        <div className="empty-bracket">
          <p>O campeonato ainda não foi iniciado. Inicie o campeonato para gerar o chaveamento.</p>
        </div>
      </div>
    );
  }

  return (
    <div className="bracket-container">
      <h3 className="bracket-title">{chaveamento.campeonatoNome} - Chaveamento</h3>
      
      <div className="bracket-rounds">
        {chaveamento.rodadas.map((rodada) => (
          <div key={rodada.numeroRodada} className="bracket-round">
            <h4 className="round-title">{rodada.nomeRodada}</h4>
            <div className="round-matches">
              {rodada.jogos.map((jogo) => (
                <div key={jogo.partidaId} className="match-card">
                  <div className="match-date">
                    {new Date(jogo.dataHora).toLocaleDateString('pt-BR')} às{' '}
                    {new Date(jogo.dataHora).toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })}
                  </div>
                  <div className="match-teams">
                    <div 
                      className={`team ${jogo.vencedorId === jogo.equipe1?.equipeId ? 'winner' : ''}`}
                      onClick={() => !jogo.concluido && jogo.equipe1 && handleSelecionarVencedor(jogo.partidaId, jogo.equipe1.equipeId)}
                      style={{ cursor: !jogo.concluido && jogo.equipe1 ? 'pointer' : 'default' }}
                    >
                      {jogo.equipe1?.nome || 'A definir'}
                      {!jogo.concluido && jogo.equipe1 && <span className="select-hint"> (clique para selecionar)</span>}
                    </div>
                    <div className="vs">VS</div>
                    <div 
                      className={`team ${jogo.vencedorId === jogo.equipe2?.equipeId ? 'winner' : ''}`}
                      onClick={() => !jogo.concluido && jogo.equipe2 && handleSelecionarVencedor(jogo.partidaId, jogo.equipe2.equipeId)}
                      style={{ cursor: !jogo.concluido && jogo.equipe2 ? 'pointer' : 'default' }}
                    >
                      {jogo.equipe2?.nome || 'A definir'}
                      {!jogo.concluido && jogo.equipe2 && <span className="select-hint"> (clique para selecionar)</span>}
                    </div>
                  </div>
                  {jogo.concluido && (
                    <div className="match-status">Concluído</div>
                  )}
                  {loading && <div className="loading-indicator">Processando...</div>}
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
