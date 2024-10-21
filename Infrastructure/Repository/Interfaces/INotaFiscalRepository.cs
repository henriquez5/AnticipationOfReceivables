using Domain.Entities;

namespace Infrastructure.Repository.Interfaces
{
    public interface INotaFiscalRepository
    {
        Task<List<NotaFiscal>> ObterTodasNotasFiscais();

        Task<NotaFiscal> ObterNotaFiscalPorId(int id);

        Task<List<NotaFiscal>> ObterTodasNotasFiscaisPorCNPJ(string cnpj);

        Task<NotaFiscal> InserirNotaFiscal(NotaFiscal input);

        Task DeletarNotaFiscal(int id);
    }
}
