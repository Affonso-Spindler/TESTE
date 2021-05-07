using System.Collections.Generic;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public interface ICons_ClienteRepository : IRepository<Cons_Cliente>
    {
        IEnumerable<Cons_Cliente> GetByFaixa(int ini, int fim);
        IEnumerable<Cons_Cliente> GetByTiers();
    }
}
