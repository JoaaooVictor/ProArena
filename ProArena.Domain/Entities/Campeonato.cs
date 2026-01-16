namespace ProArena.Domain.Entities
{
    public class Campeonato
    {
        public int CampeonatoId { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataInicio { get; set; }
        public virtual List<Equipe> Equipes { get; set; } = null!;
        public virtual List<Partida> Partidas { get; set; } = null!;
    }
}
