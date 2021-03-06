using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> GetByRegiao(string uf);
        IEnumerable<Cliente> GetByRegiao(string uf, string cidade);
    }
}
