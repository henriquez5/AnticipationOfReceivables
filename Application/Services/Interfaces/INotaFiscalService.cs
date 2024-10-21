using Application.DTOs.Responses;
using Application.DTOs.Requests;

namespace Application.Services.Interfaces
{
    public interface INotaFiscalService
    {
        Task<List<ConsultaNotaFiscalResponse>> ObterTodasNotasFiscais();

        Task<List<ConsultaNotaFiscalResponse>> ObterTodasNotasFiscaisPorCNPJ(string cnpj);

        Task<ConsultaNotaFiscalResponse> InserirNotaFiscal(CriarNotaFiscalRequest input);

    }
}
