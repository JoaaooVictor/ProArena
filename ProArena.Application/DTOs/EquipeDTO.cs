using ProArena.Domain.Entities;

namespace ProArena.Application.DTOs
{
    public class EquipeDTO
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NomeExibicao { get; set; } = string.Empty;
        public int? CampeonatoId { get; set; }
        public List<JogadorDTO> Jogadores { get; set; } = new();
    }

    public class JogadorDTO
    {
        public int JogadorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Ativo { get; set; }
    }
}
