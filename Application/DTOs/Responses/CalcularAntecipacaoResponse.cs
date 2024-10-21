namespace Application.DTOs.Responses
{
    public class CalcularAntecipacaoResponse
    {
        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public decimal? Limite { get; set; }

        public List<ConsultaNotaFiscal> NotasFiscais { get; set; }

        public decimal TotalLiquido { get; set; }

        public decimal TotalBruto { get; set; }

    }

    public class ConsultaNotaFiscal
    {
        public int Numero { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal ValorLiquido { get; set; }
    }
}
