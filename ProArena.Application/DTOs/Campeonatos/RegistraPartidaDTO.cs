namespace ProArena.Application.DTOs.Campeonatos
{
    public class RegistraPartidaDTO
    {
        public DateTime DataHora { get; set; }
        public int CampeonatoId { get; set; }
        public List<int> EquipeIds { get; set; } = new();
    }
}
