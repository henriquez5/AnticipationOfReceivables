namespace Application.DTOs.Responses
{
    public class ConsultaEmpresaResponse
    {
        public int? Id { get; set; }

        public string? CNPJ { get; set; }

        public string? Nome { get; set; }

        public decimal? FaturamentoMensal { get; set; }

        public string? Ramo { get; set; }

        public decimal? Limite { get; set; }
    }
}
