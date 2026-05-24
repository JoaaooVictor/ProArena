namespace ProArena.Application.DTOs
{
    public class ResumoFluxoCaixaDTO
    {
        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal Saldo { get; set; }
        public int QuantidadeMovimentacoes { get; set; }
    }
}
