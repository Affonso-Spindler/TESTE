namespace TestePMWEB.Repository
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }

        IPedidoRepository PedidoRepository { get; }

        IAPI_LogRepository API_LogRepository { get; }

        void Commit();
    }
}
