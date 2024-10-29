using Domain.Enums;

namespace Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public decimal FaturamentoMensal { get; set; }

        public RamoEmpresa Ramo { get; set; }

        public decimal? Limite { get; set; }

        public ICollection<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();
    }
}
