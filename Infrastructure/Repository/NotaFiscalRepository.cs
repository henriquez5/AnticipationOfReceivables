using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly DatabaseContext _context;

        public NotaFiscalRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<NotaFiscal>> ObterTodasNotasFiscais()
        {
            return await _context.NotaFiscal
                .Include(x => x.Empresa)
                .ToListAsync();
        }

        public async Task<NotaFiscal> ObterNotaFiscalPorId(int id)
        {
            return await _context.NotaFiscal.FindAsync(id);
        }

        public async Task<List<NotaFiscal>> ObterTodasNotasFiscaisPorCNPJ(string cnpj)
        {
            return await _context.NotaFiscal
                .Include(x => x.Empresa)
                .Where(nf => nf.Cnpj.Contains(cnpj))
                .ToListAsync();
        }

        public async Task<NotaFiscal> InserirNotaFiscal(NotaFiscal input)
        {
            try
            {
                await _context.AddAsync(input);
                await _context.SaveChangesAsync();

                return input;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletarNotaFiscal(int id)
        {
            try
            {
                var notaFiscal = await _context.NotaFiscal.FindAsync(id);
                if (notaFiscal != null)
                {
                    _context.NotaFiscal.Remove(notaFiscal);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
