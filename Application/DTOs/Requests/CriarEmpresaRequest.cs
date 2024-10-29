using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests
{
    public class CriarEmpresaRequest
    {
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"^(\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2})$",
            ErrorMessage = "O CNPJ deve estar no formato XX.XXX.XXX/XXXX-XX")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O Faturamento Mensal deve ser maior que 0.")]
        public decimal FaturamentoMensal { get; set; }

        public string Ramo { get; set; }
    }
}
