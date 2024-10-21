using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> ObterTodasEmpresas();

        Task<Empresa> ObterEmpresaPorId(int id);

        Task<Empresa> ObterEmpresaPorCNPJ(string cnpj);

        Task<Empresa> InserirEmpresa(Empresa input);

        Task<Empresa> AtualizarEmpresa(Empresa input);

        Task DeletarEmpresa(int id);
    }
}
