namespace ProArena.Application.DTOs
{
    public class RegistraJogadorDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
