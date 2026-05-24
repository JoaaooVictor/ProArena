namespace ProArena.Application.DTOs.Jogadores
{
	public class JogadorDTO
	{
		public int JogadorId { get; set; }
		public string Nome { get; set; } = string.Empty;
		public string Cpf { get; set; } = string.Empty;
		public int Idade { get; set; }
		public bool Ativo { get; set; }
	}
}
