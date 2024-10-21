namespace Application.DTOs.Responses
{
    public class ConsultaNotaFiscalResponse
    {
        public string? Cnpj { get; set; }

        public int Numero { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        public int? EmpresaId { get; set; }

        public decimal ValorBruto { get; set; }
    }
}
