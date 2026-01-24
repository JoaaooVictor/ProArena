namespace ProArena.Domain.Entities
{
    public class Jogador
    {
        public int JogadorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public virtual List<Equipe> Equipes { get; set; } = new List<Equipe>();
    }
}
