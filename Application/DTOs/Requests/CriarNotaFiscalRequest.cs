using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests
{
    public class CriarNotaFiscalRequest
    {
        public string? Cnpj { get; set; }

        public int Numero { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da nota deve ser maior do que 0.")]
        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }
    }
}
