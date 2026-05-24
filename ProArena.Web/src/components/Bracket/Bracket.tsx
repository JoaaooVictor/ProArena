import { IChaveamento, IRodada, IJogo } from '../../interfaces/IChaveamento';

interface BracketProps {
  chaveamento: IChaveamento;
}

export default function Bracket({ chaveamento }: BracketProps) {
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
                    <div className={`team ${jogo.vencedorId === jogo.equipe1?.equipeId ? 'winner' : ''}`}>
                      {jogo.equipe1?.nome || 'A definir'}
                    </div>
                    <div className="vs">VS</div>
                    <div className={`team ${jogo.vencedorId === jogo.equipe2?.equipeId ? 'winner' : ''}`}>
                      {jogo.equipe2?.nome || 'A definir'}
                    </div>
                  </div>
                  {jogo.concluido && (
                    <div className="match-status">Concluído</div>
                  )}
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
