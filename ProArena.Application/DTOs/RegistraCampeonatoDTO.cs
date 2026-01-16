namespace ProArena.Application.DTOs
{
    public class RegistraCampeonatoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public List<RegistraEquipeDTO>? Equipes { get; set; }
        public List<RegistraPartidaDTO>? Partidas { get; set; }
    }
}
