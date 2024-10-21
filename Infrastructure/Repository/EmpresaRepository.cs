using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DatabaseContext _context;

        public EmpresaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> ObterTodasEmpresas()
        {
            return await _context.Empresa.ToListAsync();
        }

        public async Task<Empresa> ObterEmpresaPorId(int id)
        {
            return await _context.Empresa.FindAsync(id);
        }

        public async Task<Empresa> ObterEmpresaPorCNPJ(string cnpj)
        {
            return await _context.Empresa
                .Where(em => em.CNPJ.Contains(cnpj)).FirstAsync();
        }

        public async Task<Empresa> InserirEmpresa(Empresa input)
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

        public async Task<Empresa> AtualizarEmpresa(Empresa input)
        {
            try
            {
                _context.Update(input);
                await _context.SaveChangesAsync();

                return input;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletarEmpresa(int id)
        {
            try
            {
                var empresa = await _context.Empresa.FindAsync(id);
                if (empresa != null)
                {
                    _context.Empresa.Remove(empresa);
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
