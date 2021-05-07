using TestePMWEB.Context;

namespace TestePMWEB.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PedidoRepository _pedidoRepo;
        private ClienteRepository _clienteRepo;
        private API_LogRepository _logRepo;
        private Cons_ClienteRepository _consCliente;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPedidoRepository PedidoRepository
        {
            get
            {
                return _pedidoRepo = _pedidoRepo ?? new PedidoRepository(_context);
            }
        }

        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
            }
        }

        public IAPI_LogRepository API_LogRepository
        {
            get
            {
                return _logRepo = _logRepo ?? new API_LogRepository(_context);
            }
        }

        public ICons_ClienteRepository Cons_ClienteRepository
        {
            get
            {
                return _consCliente = _consCliente ?? new Cons_ClienteRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
