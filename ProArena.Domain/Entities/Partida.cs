namespace ProArena.Domain.Entities
{
    public class Partida
    {
        public int PartidaId { get; set; }
        public DateTime DataHora { get; set; }

        public int CampeonatoId { get; set; }
        public Campeonato Campeonato { get; set; } = null!;

        public int EquipeId { get; set; }
        public virtual List<Equipe> Equipe { get; set; } = null!;
    }
}
