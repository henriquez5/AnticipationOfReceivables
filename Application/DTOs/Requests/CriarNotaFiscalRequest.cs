using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests
{
    public class CriarNotaFiscalRequest
    {
        [RegularExpression(@"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}|\d{14}", ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; }

        public int Numero { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

    }
}
