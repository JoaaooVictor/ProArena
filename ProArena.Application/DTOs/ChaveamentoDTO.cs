namespace ProArena.Application.DTOs
{
    public class ChaveamentoDTO
    {
        public int CampeonatoId { get; set; }
        public string CampeonatoNome { get; set; } = string.Empty;
        public List<RodadaDTO> Rodadas { get; set; } = new();
    }

    public class RodadaDTO
    {
        public int NumeroRodada { get; set; }
        public string NomeRodada { get; set; } = string.Empty;
        public List<JogoDTO> Jogos { get; set; } = new();
    }

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
