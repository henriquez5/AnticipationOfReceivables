using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repository.Interfaces;

namespace Application.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository, IEmpresaRepository empresaRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<List<ConsultaNotaFiscalResponse>> ObterTodasNotasFiscais()
        {
            try
            {
                var obterNotasFiscais = await _notaFiscalRepository.ObterTodasNotasFiscais();

                var notasFiscais = obterNotasFiscais.Select(nf => new ConsultaNotaFiscalResponse
                {
                    Cnpj = nf.Empresa.CNPJ,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,

                }).ToList();

                return notasFiscais;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ConsultaNotaFiscalResponse>> ObterTodasNotasFiscaisPorCNPJ(string cnpj)
        {
            try
            {
                var obterNotasFiscais = await _notaFiscalRepository.ObterTodasNotasFiscaisPorCNPJ(cnpj);

                if (obterNotasFiscais == null || !obterNotasFiscais.Any())
                {
                    throw new KeyNotFoundException($"NotaFiscal com CNPJ: {cnpj} não foi encontrado.");
                }

                var notasFiscais = obterNotasFiscais.Select(nf => new ConsultaNotaFiscalResponse
                {
                    Cnpj = nf.Empresa.CNPJ,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,
                    
                }).ToList();

                return notasFiscais;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConsultaNotaFiscalResponse> InserirNotaFiscal(CriarNotaFiscalRequest input)
        {
            var empresa = await _empresaRepository.ObterEmpresaPorCNPJ(input.Cnpj);

            if(empresa == null)
                throw new KeyNotFoundException($"Empresa com CNPJ: {input.Cnpj} não foi encontrado.");

            var newNotaFiscal = new NotaFiscal
            {
                Cnpj = empresa.CNPJ,
                Numero = input.Numero,
                Valor = input.Valor,
                DataVencimento = input.DataVencimento,

                EmpresaId = empresa.Id
            };

            var nf = await _notaFiscalRepository.InserirNotaFiscal(newNotaFiscal);

            return new ConsultaNotaFiscalResponse
            {
                Cnpj = nf.Empresa.CNPJ,
                Numero = nf.Numero,
                Valor = nf.Valor,
                DataVencimento = nf.DataVencimento,
                EmpresaId = nf.Empresa.Id,
                ValorBruto = nf.Valor
            };

        }
    }
}