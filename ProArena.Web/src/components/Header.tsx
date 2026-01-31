export default function Header() {
  return (
    <div className="header">
      <div className="search">
        <span style={{ color: 'var(--muted)' }}>🔎</span>
        <input placeholder="Busque por campeonatos, jogadores, partidas..." />
      </div>

      <div className="actions">
        <button className="icon-btn">🔔</button>
        <button className="icon-btn">?</button>
      </div>
    </div>
  )
}
