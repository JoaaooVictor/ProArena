namespace ProArena.Application.DTOs.Equipes
{
    public class RegistraEquipeDTO
    {
        public string Nome { get; set; } = string.Empty;
        public List<int> JogadorIds { get; set; } = new();
        public int CampeonatoId { get; set; }
    }
}
