using ProArena.Domain.Enums;

namespace ProArena.Application.DTOs
{
    public class RegistraMovimentacaoFinanceiraDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoMovimentacaoFinanceiraEnum Tipo { get; set; }
        public string? Categoria { get; set; }
        public int? CampeonatoId { get; set; }
    }
}
