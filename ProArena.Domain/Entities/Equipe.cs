namespace ProArena.Domain.Entities
{
    public class Equipe
    {
        public int EquipeId { get; set; }
        public string? Nome { get; set; }
        public virtual List<Jogador> Jogadores { get; set; } = new List<Jogador>();
    }
}
