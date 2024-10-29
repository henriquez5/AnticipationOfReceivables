using Application.DTOs.Responses;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repository.Interfaces;

namespace Application.Services
{
    public class CalcularAntecipacaoService : ICalcularAntecipacaoService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public CalcularAntecipacaoService(IEmpresaRepository empresaRepository, INotaFiscalRepository notaFiscalRepository)
        {
            _empresaRepository = empresaRepository;
            _notaFiscalRepository = notaFiscalRepository;
        }

        public async Task<CalcularAntecipacaoResponse> CalcularAntecipacao(string cnpj)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            {
                throw new FormatException("O CNPJ deve estar no formato 00.000.000/0000-00.");
            }

            var empresa = await _empresaRepository.ObterEmpresaPorCNPJ(cnpj);
            if (empresa == null)
            {
                throw new KeyNotFoundException($"Empresa com CNPJ: {cnpj} não foi encontrada.");
            }

            var notasFiscais = await _notaFiscalRepository.ObterTodasNotasFiscaisPorCNPJ(cnpj);
            if (notasFiscais == null || !notasFiscais.Any())
            {
                throw new KeyNotFoundException($"Nenhuma nota fiscal encontrada para o CNPJ: {cnpj}.");
            }

            var response = new CalcularAntecipacaoResponse
            {
                Nome = empresa.Nome,
                CNPJ = empresa.CNPJ,
                Limite = empresa.Limite,
                NotasFiscais = new List<ConsultaNotaFiscal>()
            };

            foreach (var nf in notasFiscais.OrderBy(n => n.Numero))
            {
                var notaFiscalResponse = new ConsultaNotaFiscal
                {
                    Numero = nf.Numero,
                    ValorBruto = nf.Valor,
                    ValorLiquido = Math.Round(CalcularValorLiquido(nf), 2)
                };

                response.NotasFiscais.Add(notaFiscalResponse);
            }

            response.TotalBruto = Math.Round(notasFiscais.Sum(nf => nf.Valor), 2);

            if (response.TotalBruto > empresa.Limite)
                throw new InvalidOperationException($"Operação não permitida pois o valor total das NF : {response.TotalBruto} , ultrapassa o limite da empresa {empresa.Limite}");

            response.TotalLiquido = Math.Round(notasFiscais.Sum(CalcularValorLiquido), 2);

            return response;
        }


        public decimal CalcularValorLiquido(NotaFiscal nf)
        {
            int prazo = (int)(nf.DataVencimento - DateTime.Now).TotalDays;

            if (prazo <= 0)
                throw new ArgumentException("Data de vencimento inválida.");

            decimal taxaMensal = 0.0400m;

            decimal desagio = nf.Valor / (decimal)Math.Pow((double)(1 + taxaMensal), prazo / 30.0);

            return desagio;
        }
    }
}

