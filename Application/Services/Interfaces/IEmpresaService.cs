using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<List<ConsultaEmpresaResponse>> ObterTodasEmpresas();

        Task<ConsultaEmpresaResponse> ObterEmpresaPorId(int id);

        Task<ConsultaEmpresaResponse> ObterEmpresaPorCNPJ(string cnpj);

        Task<ConsultaEmpresaResponse> InserirEmpresa(CriarEmpresaRequest input);
    }
}
