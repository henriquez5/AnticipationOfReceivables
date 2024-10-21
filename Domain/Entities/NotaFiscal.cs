namespace Domain.Entities
{
    public class NotaFiscal
    {
        public int Id {  get; set; }
        public string? Cnpj { get; set; }
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }
    }
}
