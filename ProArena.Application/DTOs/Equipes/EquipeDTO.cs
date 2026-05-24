using ProArena.Application.DTOs.Jogadores;
using ProArena.Domain.Entities;

namespace ProArena.Application.DTOs.Equipes
{
    public class EquipeDTO
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NomeExibicao { get; set; } = string.Empty;
        public int? CampeonatoId { get; set; }
        public List<JogadorDTO> Jogadores { get; set; } = new();
    }
}
