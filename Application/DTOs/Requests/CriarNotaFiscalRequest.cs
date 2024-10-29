namespace Application.DTOs.Requests
{
    public class CriarNotaFiscalRequest
    {
        public string? Cnpj { get; set; }
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
