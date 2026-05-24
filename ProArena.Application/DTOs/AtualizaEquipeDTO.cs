namespace ProArena.Application.DTOs
{
    public class AtualizaEquipeDTO
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<int> JogadorIds { get; set; } = new();
    }
}
