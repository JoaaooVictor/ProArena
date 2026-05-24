namespace ProArena.Application.DTOs.Campeonatos
{
	public class RodadaDTO
	{
		public int NumeroRodada { get; set; }
		public string NomeRodada { get; set; } = string.Empty;
		public List<JogoDTO> Jogos { get; set; } = new();
	}
}
