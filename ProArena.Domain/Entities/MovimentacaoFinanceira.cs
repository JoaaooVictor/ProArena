using ProArena.Domain.Enums;

namespace ProArena.Domain.Entities
{
    public class MovimentacaoFinanceira
    {
        public int MovimentacaoFinanceiraId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoMovimentacaoFinanceiraEnum Tipo { get; set; }
        public string? Categoria { get; set; }
        public int? CampeonatoId { get; set; }
        public virtual Campeonato? Campeonato { get; set; }
    }
}
