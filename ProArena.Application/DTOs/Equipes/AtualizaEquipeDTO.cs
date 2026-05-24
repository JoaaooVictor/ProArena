namespace ProArena.Application.DTOs.Equipes
{
    public class AtualizaEquipeDTO
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<int> JogadorIds { get; set; } = new();
    }
}
