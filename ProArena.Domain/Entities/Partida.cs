namespace ProArena.Domain.Entities
{
    public class Partida
    {
        public int PartidaId { get; set; }
        public int ResultadoEquipeA { get; set; }
        public int ResultadoEquipeB { get; set; }
        public DateTime DataHora { get; set; }

        public int CampeonatoId { get; set; }
        public Campeonato Campeonato { get; set; } = null!;

        public int EquipeAId { get; set; }
        public virtual Equipe EquipeA { get; set; } = null!;

        public int EquipeBId { get; set; }
        public virtual Equipe EquipeB { get; set; } = null!;
    }
}
