namespace Application.DTOs.Requests
{
    public class CriarEmpresaRequest
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public decimal FaturamentoMensal { get; set; }
        public string Ramo { get; set; }
    }
}
