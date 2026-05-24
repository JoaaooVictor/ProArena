namespace ProArena.Application.DTOs.Campeonatos
{
    public class ChaveamentoDTO
    {
        public int CampeonatoId { get; set; }
        public string CampeonatoNome { get; set; } = string.Empty;
        public List<RodadaDTO> Rodadas { get; set; } = new();
    }
}
