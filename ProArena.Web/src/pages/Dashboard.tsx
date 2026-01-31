import '../styles/dashboard.css'

export default function Dashboard() {
  return (
    <div className="dashboard">
      <div style={{ display:'flex', alignItems:'center', gap:12 }}>
        <div>
          <h1>Dashboard</h1>
          <p className="subtitle">Você possui três campeoatos ativos abertos para inscrições.</p>
        </div>
      </div>

      <div className="stats-grid">
        <div className="card">
          <div className="label">Campeonatos Ativos<span>🏆</span></div>
          <div className="value">12</div>
          <div className="meta">+20% from last month</div>
        </div>

        <div className="card">
          <div className="label">Times Registrados <span>👥</span></div>
          <div className="value">156</div>
          <div className="meta">+15% new registrations</div>
        </div>

        <div className="card">
          <div className="label">Resultados Pendentes<span>⏳</span></div>
          <div className="value">8</div>
          <div className="meta" style={{ color:'#fb923c' }}>4 overdue</div>
        </div>

        <div className="card">
          <div className="label">Total entradas<span>💵</span></div>
          <div className="value">R$ 4.280</div>
          <div className="meta">+12% vs goal</div>
        </div>
      </div>

      <div className="grid-2">
        <div className="section">
          <h2>Chave Mata-mata</h2>
          <div className="bracket">
            <div className="round">
              <div className="match">
                <div className="team"><span>Strikers FC</span><small>1</small></div>
                <div className="team"><span>Titan Squad</span><small>3</small></div>
              </div>
              <div className="match">
                <div className="team"><span>Phoenix XI</span><small>2</small></div>
                <div className="team"><span>Blue Jays</span><small>0</small></div>
              </div>
            </div>

            <div className="round">
              <div className="match">
                <div className="team"><span>Titan Squad</span><small>—</small></div>
                <div className="team"><span>Phoenix XI</span><small>—</small></div>
              </div>
            </div>

            <div className="round">
              <div className="match">
                <div className="team"><span>TBD Match 13</span><small>—</small></div>
                <div className="team"><span>TBD Match 14</span><small>—</small></div>
              </div>
            </div>
          </div>
        </div>

        <div className="section">
          <h2>Ações</h2>
          <div className="list">
            <div className="list-item">
              <div className="left">
                <div className="title">Add jogador</div>
                <div className="desc">Criar novo time</div>
              </div>
              <span>➕</span>
            </div>
            <div className="list-item">
              <div className="left">
                <div className="title">Exportar</div>
                <div className="desc">Download times</div>
              </div>
              <span>⬇️</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
