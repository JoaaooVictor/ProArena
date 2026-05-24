namespace ProArena.Application.DTOs.Campeonatos
{
    public class AtualizaPartidaDTO
    {
        public int PartidaId { get; set; }
        public DateTime DataHora { get; set; }
        public List<int> EquipeIds { get; set; } = new();
    }
}
