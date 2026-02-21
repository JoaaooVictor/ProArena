namespace ProArena.Domain.Entities
{
    public class Equipe
    {
        public int EquipeId { get; set; }
        public string? Nome { get; set; }
        public string NomeExibicao
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Nome))
                {
                    return Nome;
                }

                return string.Join("/", Jogadores.Select(j => j.Nome));
            }
        }

        public int? CampeonatoId { get; set; }
        public virtual List<Inscricao> Inscricao { get; set; } = null!;
        public virtual List<Jogador> Jogadores { get; set; } = null!;
        public virtual List<Partida> Partidas { get; set; } = null!;
    }
}
