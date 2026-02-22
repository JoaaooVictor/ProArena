import '../CardDashboard/CardDashboard.css';

type CardProps = {
  titulo: string;
  icone?: string;          
  quantidade: number;
  variacaoTexto?: string;
  variacaoPositiva?: boolean; 
}

export function CardDashboard({
  titulo,
  icone,
  quantidade,
  variacaoTexto,
  variacaoPositiva = true,
}: CardProps) {
  return (
    <div className="card-box">
      <div className="card-header">
        <span className="card-title">{titulo}</span>
        {icone && <i className={`card-icon ${icone}`} aria-hidden="true"></i>}
      </div>

      <div className="card-quantidade">{quantidade}</div>

      {variacaoTexto && (
        <div className={`card-footer ${variacaoPositiva ? 'is-positive' : 'is-negative'}`}>
          <span className="card-arrow" aria-hidden="true">↗</span>
          <span>{variacaoTexto}</span>
        </div>
      )}
    </div>
  );
}