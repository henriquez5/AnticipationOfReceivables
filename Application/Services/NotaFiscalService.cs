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
               var obterNotasFiscais = await _notaFiscalRepository.ObterTodasNotasFiscais();

                // Mapear as notas fiscais para a resposta
                return obterNotasFiscais.Select(nf => new ConsultaNotaFiscalResponse
                {
                    Id = nf.Id,
                    Cnpj = nf.Empresa.CNPJ,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,
                    EmpresaId = nf.EmpresaId,
                }).ToList();
        }

        public async Task<List<ConsultaNotaFiscalResponse>> ObterTodasNotasFiscaisPorCNPJ(string cnpj)
        {
            try
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
                {
                    throw new FormatException("O CNPJ deve estar no formato 00.000.000/0000-00.");
                }

                var obterNotasFiscais = await _notaFiscalRepository.ObterTodasNotasFiscaisPorCNPJ(cnpj);

                if (obterNotasFiscais == null || !obterNotasFiscais.Any())
                {
                    throw new KeyNotFoundException($"NotaFiscal com CNPJ: {cnpj} não foi encontrada.");
                }

                var notasFiscais = obterNotasFiscais.Select(nf => new ConsultaNotaFiscalResponse
                {
                    Id = nf.Id,
                    Cnpj = nf.Empresa.CNPJ,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,
                    EmpresaId = nf.EmpresaId
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
            if (!System.Text.RegularExpressions.Regex.IsMatch(input.Cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            {
                throw new FormatException("O CNPJ deve estar no formato 00.000.000/0000-00.");
            }

            var empresa = await _empresaRepository.ObterEmpresaPorCNPJ(input.Cnpj);

            if(empresa == null)
                throw new KeyNotFoundException($"Empresa com CNPJ: {input.Cnpj} não foi encontrado.");

            if (input.DataVencimento <= DateTime.Now)
            {
                throw new ArgumentException("A data de vencimento não pode ser menor que a data atual.");
            }

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
                Id= nf.Id,
                Cnpj = nf.Empresa.CNPJ,
                Numero = nf.Numero,
                Valor = nf.Valor,
                DataVencimento = nf.DataVencimento,
                EmpresaId = nf.Empresa.Id
            };

        }

        public async Task<bool> DeletarNotaFiscal(int id)
        {
            try
            {
                var excludeNota = await _notaFiscalRepository.DeletarNotaFiscal(id);

                if (excludeNota == false) throw new KeyNotFoundException("Nota fiscal não encontrada para exclusão.");

                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
