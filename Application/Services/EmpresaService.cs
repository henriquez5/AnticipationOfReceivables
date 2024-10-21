using Application.Services.Interfaces;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Infrastructure.Repository.Interfaces;
using Domain.Entities;

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
                    CNPJ = em.CNPJ,
                    Ramo = em.Ramo,
                    Nome = em.Nome
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
                    CNPJ = empresa.CNPJ,
                    Nome = empresa.Nome,
                    Ramo = empresa.Ramo
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
                var empresa = await _empresaRepository.ObterEmpresaPorCNPJ(cnpj);

                if (empresa == null)
                {
                    throw new KeyNotFoundException($"Empresa com CNPJ: {cnpj} não foi encontrado.");
                }

                return new ConsultaEmpresaResponse
                {
                    CNPJ = empresa.CNPJ,
                    Nome = empresa.Nome,
                    Ramo = empresa.Ramo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConsultaEmpresaResponse> InserirEmpresa(CriarEmpresaRequest input)
        {
            var newEmpresa = new Empresa
            {
                CNPJ = input.CNPJ,
                Nome = input.Nome,
                Ramo = input.Ramo,
                FaturamentoMensal = input.FaturamentoMensal,
                Limite = CalcularLimite(input.FaturamentoMensal,input.Ramo)
            };

            var empresa = await _empresaRepository.InserirEmpresa(newEmpresa);

            return new ConsultaEmpresaResponse
            {
                Id = empresa.Id,
                CNPJ = empresa.CNPJ,
                Nome = empresa.Nome,
                Ramo = empresa.Ramo,
                Limite= empresa.Limite,
                FaturamentoMensal = empresa.FaturamentoMensal
            };
        }

        public decimal CalcularLimite(decimal FaturamentoMensal, string Ramo)
        {
            decimal Limite = 0; 

            if (FaturamentoMensal >= 10000 && FaturamentoMensal <= 50000)
            {
                Limite = FaturamentoMensal * 0.50m;
            }
            else if (FaturamentoMensal > 50000 && FaturamentoMensal <= 100000)
            {
                Limite = Ramo == "Serviços" ? FaturamentoMensal * 0.55m : FaturamentoMensal * 0.60m;
            }
            else if (FaturamentoMensal > 100000)
            {
                Limite = Ramo == "Serviços" ? FaturamentoMensal * 0.60m : FaturamentoMensal * 0.65m;
            }

            return Limite;
        }
    }
}
