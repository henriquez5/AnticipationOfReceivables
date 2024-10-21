using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests
{
    public class CriarNotaFiscalRequest
    {
        public string? Cnpj { get; set; }
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int EmpresaId { get; internal set; }
    }
}
