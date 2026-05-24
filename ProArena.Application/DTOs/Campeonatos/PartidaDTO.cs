using ProArena.Application.DTOs.Equipes;

namespace ProArena.Application.DTOs.Campeonatos
{
    public class PartidaDTO
    {
        public int PartidaId { get; set; }
        public DateTime DataHora { get; set; }
        public int CampeonatoId { get; set; }
        public List<EquipeDTO> Equipes { get; set; } = new();
    }
}
