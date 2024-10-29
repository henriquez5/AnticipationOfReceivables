using Application.Services.Interfaces;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Infrastructure.Repository.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository) 
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<List<ConsultaEmpresaResponse>> ObterTodasEmpresas()
        {
            try
            {
                var empresas = await _empresaRepository.ObterTodasEmpresas();

                var listaEmpresas = empresas.Select(em => new ConsultaEmpresaResponse
                {
                    Id = em.Id,
                    CNPJ = em.CNPJ,
                    Ramo = em.Ramo.ToString(),
                    FaturamentoMensal = em.FaturamentoMensal,
                    Nome = em.Nome,
                    Limite = em.Limite
                }).ToList();

                return listaEmpresas;
            }
            catch(Exception)
            {
                throw;
            } 
        }

        public async Task<ConsultaEmpresaResponse> ObterEmpresaPorId(int id)
        {
            try
            {
                var empresa = await _empresaRepository.ObterEmpresaPorId(id);

                if(empresa == null)
                {
                    throw new KeyNotFoundException($"Empresa com ID {id} não foi encontrado.");
                }

                return new ConsultaEmpresaResponse
                {
                    Id = empresa.Id,
                    CNPJ = empresa.CNPJ,
                    Ramo = empresa.Ramo.ToString(),
                    FaturamentoMensal = empresa.FaturamentoMensal,
                    Nome = empresa.Nome,
                    Limite = empresa.Limite
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ConsultaEmpresaResponse> ObterEmpresaPorCNPJ(string cnpj)
        {
            try
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
                {
                    throw new FormatException("O CNPJ deve estar no formato 00.000.000/0000-00.");
                }

                var empresa = await _empresaRepository.ObterEmpresaPorCNPJ(cnpj);

                if (empresa == null)
                {
                    throw new KeyNotFoundException($"Empresa com CNPJ: {cnpj} não foi encontrado.");
                }

                return new ConsultaEmpresaResponse
                {
                    CNPJ = empresa.CNPJ,
                    Nome = empresa.Nome,
                    Ramo = empresa.Ramo.ToString()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConsultaEmpresaResponse> InserirEmpresa(CriarEmpresaRequest input)
        {
            if (!Enum.TryParse<RamoEmpresa>(input.Ramo, true, out var ramoEmpresa))
            {
                throw new ArgumentException("O ramo deve ser 'Serviços' ou 'Produtos'.");
            }

            var empresaExiste = await _empresaRepository.ObterEmpresaPorCNPJ(input.CNPJ);

            if (empresaExiste != null)
                throw new InvalidOperationException("Empresa com CNPJ informado já existe");

            var newEmpresa = new Empresa
            {
                CNPJ = input.CNPJ,
                Nome = input.Nome,
                Ramo = ramoEmpresa,
                FaturamentoMensal = input.FaturamentoMensal,
                Limite = CalcularLimite(input.FaturamentoMensal, ramoEmpresa)
            };

            var empresa = await _empresaRepository.InserirEmpresa(newEmpresa);

            return new ConsultaEmpresaResponse
            {
                Id = empresa.Id,
                CNPJ = empresa.CNPJ,
                Nome = empresa.Nome,
                Ramo = empresa.Ramo.ToString(),
                Limite= empresa.Limite,
                FaturamentoMensal = empresa.FaturamentoMensal
            };
        }

        public decimal CalcularLimite(decimal FaturamentoMensal, RamoEmpresa Ramo)
        {
            decimal Limite = 0;

            if (FaturamentoMensal >= 10000 && FaturamentoMensal <= 50000)
            {
                Limite = FaturamentoMensal * 0.50m;
            }
            else if (FaturamentoMensal > 50000 && FaturamentoMensal <= 100000)
            {
                Limite = Ramo == RamoEmpresa.Serviços ? FaturamentoMensal * 0.55m : FaturamentoMensal * 0.60m;
            }
            else if (FaturamentoMensal > 100000)
            {
                Limite = Ramo == RamoEmpresa.Serviços ? FaturamentoMensal * 0.60m : FaturamentoMensal * 0.65m;
            }

            return Limite;
        }
    }
}
