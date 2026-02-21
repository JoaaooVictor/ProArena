using System.Data;

namespace ProArena.Domain.Entities
{
    public class Inscricao
    {
        public int InscricaoId { get; set; }
        public DateTime DataInscricao { get; set; }

        public int CampeonatoId { get; set; }
        public virtual Campeonato Campeonato { get; set; } = null!;

        public int EquipeId { get; set; }
        public virtual Equipe Equipe { get; set; } = null!;
    }
}
