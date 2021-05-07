using System.Collections.Generic;
using System.Linq;
using TestePMWEB.Context;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext contexto) : base(contexto)
        {

        }

        public IEnumerable<Cliente> GetByRegiao(string uf)
        {
            return Get().Where(c => c.UF == uf).OrderBy(c => c.UF).ThenBy(c => c.CIDADE).ToList();
        }

        public IEnumerable<Cliente> GetByRegiao(string uf, string cidade)
        {
            return Get().Where(c=> c.UF == uf && c.CIDADE == cidade).OrderBy(c => c.UF).ThenBy(c => c.CIDADE).ToList();
        }

    }
}
