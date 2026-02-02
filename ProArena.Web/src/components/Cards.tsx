export default function Cards({titulo,valor,subtitulo}: {titulo: string, valor: number, subtitulo: string}) {
  return (
    <div className="stat-card">
      <p>{titulo}</p>
      <h2>{valor}</h2>
      <small>{subtitulo}</small>
    </div>
  )
}
