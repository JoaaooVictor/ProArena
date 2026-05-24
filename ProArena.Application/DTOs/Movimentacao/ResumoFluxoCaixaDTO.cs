namespace ProArena.Application.DTOs.Movimentacao
{
    public class ResumoFluxoCaixaDTO
    {
        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal Saldo { get; set; }
        public int QuantidadeMovimentacoes { get; set; }
    }
}
