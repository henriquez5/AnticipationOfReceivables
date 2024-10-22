using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests
{
    public class CriarEmpresaRequest
    {
        [RegularExpression(@"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}|\d{14}", ErrorMessage = "CNPJ inválido.")]
        public string CNPJ { get; set; }

        [Required]
        public string Nome { get; set; }

        public decimal FaturamentoMensal { get; set; }

        public string Ramo { get; set; }
    }
}
