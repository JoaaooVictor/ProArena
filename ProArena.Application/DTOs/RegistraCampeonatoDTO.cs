namespace ProArena.Application.DTOs
{
    public class RegistraCampeonatoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataInicioInscricao { get; set; }
        public DateTime? DataFimInscricao { get; set; }
        public List<RegistraEquipeDTO> Equipes { get; set; } = new();
        public List<RegistraPartidaDTO> Partidas { get; set; } = new();
    }
}
