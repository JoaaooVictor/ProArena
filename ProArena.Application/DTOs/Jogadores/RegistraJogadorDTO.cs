namespace ProArena.Application.DTOs.Jogadores
{
    public class RegistraJogadorDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}
