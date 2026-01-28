namespace ProArena.Application.DTOs
{
    public class AtualizaJogadorDTO
    {
        public int JogadorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Ativo { get; set; }
    }
}
