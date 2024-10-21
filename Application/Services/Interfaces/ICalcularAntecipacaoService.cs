using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICalcularAntecipacaoService
    {
        Task<CalcularAntecipacaoResponse> CalcularAntecipacao(string cnpj);

        decimal CalcularValorLiquido(NotaFiscal nf);
    }
}
