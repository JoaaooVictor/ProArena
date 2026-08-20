namespace ProArena.Domain.Entities
{
    public class Partida
    {
        public int PartidaId { get; set; }
        public DateTime DataHora { get; set; }

        public int CampeonatoId { get; set; }
        public Campeonato Campeonato { get; set; } = null!;

        public virtual List<Equipe> Equipes { get; set; } = null!;
        
        public int? VencedorId { get; set; }
        public bool Concluido { get; set; } = false;
    }
}
