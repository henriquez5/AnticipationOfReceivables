using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class NotaFiscalResponse
    {
        public int Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}
