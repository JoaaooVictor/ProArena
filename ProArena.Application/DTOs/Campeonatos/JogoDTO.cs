using ProArena.Application.DTOs.Equipes;

namespace ProArena.Application.DTOs.Campeonatos
{
	public class JogoDTO
	{
		public int PartidaId { get; set; }
		public DateTime DataHora { get; set; }
		public EquipeDTO? Equipe1 { get; set; }
		public EquipeDTO? Equipe2 { get; set; }
		public int? VencedorId { get; set; }
		public bool Concluido { get; set; }
	}
}
