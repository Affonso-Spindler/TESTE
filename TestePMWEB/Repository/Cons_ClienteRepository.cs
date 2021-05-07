using System.Collections.Generic;
using System.Linq;
using TestePMWEB.Context;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public class Cons_ClienteRepository : Repository<Cons_Cliente>, ICons_ClienteRepository
    {
        public Cons_ClienteRepository(AppDbContext contexto) : base(contexto)
        {

        }

        public IEnumerable<Cons_Cliente> GetByFaixa(int ini, int fim)
        {
            return Get().Where(c => c.FAIXA >= ini && c.FAIXA <= fim).ToList();
        }

        public IEnumerable<Cons_Cliente> GetByTiers()
        {
            return Get().OrderBy(c => c.TIERS).ToList();
        }

        public IEnumerable<Cons_Cliente> GetByTempoMedioCompras(int ini, int fim)
        {
            return Get().Where(c => c.TEMPO_MEDIOCOMPRAS >= ini && c.TEMPO_MEDIOCOMPRAS <= fim).ToList();
        }
    }
}
