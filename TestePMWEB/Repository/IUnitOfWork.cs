namespace TestePMWEB.Repository
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }

        IPedidoRepository PedidoRepository { get; }

        void Commit();
    }
}
