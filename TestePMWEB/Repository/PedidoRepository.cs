using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePMWEB.Context;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(AppDbContext contexto) : base(contexto)
        {

        }
    }
}
